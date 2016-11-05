using System;
using System.Linq;
using jackfacts.HL7.V24.Message;
using NHapi.Base.Parser;
using NHapi.Model.V24.Segment;
using NUnit.Framework;

namespace jackfacts.HL7.Tests
{
    [TestFixture]
    public class ParseMessageTests
    {
        private const string EncodingCharacters = @"^~\&";

        private const string FieldSeparator = "|";

        private readonly DateTime _fixedDate = new DateTime(2016, 7, 1, 12, 45, 12);

        protected void PopulateMessageHeaders(MSH headers, string messageType)
        {
            headers.FieldSeparator.Value = FieldSeparator;
            headers.EncodingCharacters.Value = EncodingCharacters;

            headers.SendingApplication.NamespaceID.Value = "TEST";
            headers.SendingFacility.NamespaceID.Value = "Test Facility";
            headers.ReceivingApplication.NamespaceID.Value = "VIP";

            headers.DateTimeOfMessage.TimeOfAnEvent.SetLongDateWithSecond(_fixedDate);
            headers.MessageType.MessageType.Value = messageType;

            headers.MessageControlID.Value = "636139528780284680";
            headers.ProcessingID.ProcessingID.Value = "P";
            headers.VersionID.VersionID.Value = "2.4";
        }

        [Test]
        public void CreateMFR_M01withZIS()
        {
            var mfr_m01 = new MFR_M01();

            PopulateMessageHeaders(mfr_m01.MSH, nameof(MFR_M01));

            var zisGroup = mfr_m01.AddZIS();

            var zis = zisGroup.ZIS;
            zis.Identifier.Identifier.Value = "1234";
            zis.Identifier.Text.Value = "PIR";
            zis.Identifier.NameOfCodingSystem.Value = "Hemo";

            zis.DateTimeOfCreation.TimeOfAnEvent.SetLongDateWithSecond(_fixedDate);

            zisGroup.ZIS.Procedure.Value = "TOT";

            var parser = new PipeParser();
            var encodedString = parser.Encode(mfr_m01);
            var escapedString = Escape.unescape(encodedString, new EncodingCharacters('|', '^', '~', '\\', '&'));

            Assert.AreEqual(escapedString,
                "MSH|^~\\&|TEST|Test Facility|VIP||20160701124512||MFR_M01|636139528780284680|P|2.4\rZIS|1234^PIR^Hemo|20160701124512|TOT\r");
        }

        [Test]
        public void ParseMultipleZIS()
        {
            var result = @"MSH|^~\&|HIP|Blood Lab|LIS|XYZ Laboratory|20160818131019376||MFR^M01|VMSG14|P|2.4|
        MSA|AA|12345678|
        ZIS|2^PR^STAIN|20160817082024472|Hemo|
        ZIS|58^DAP^STAIN|20150607122237649|DAB|
        ZIS|103^TIN^STAIN|20150916122237649|DAB|
        ZIS|126^WN^STAIN|20160104122237649|DAB|";

            var parser = new PipeParser();
            var escapedResult = Escape.unescape(result, new EncodingCharacters('|', '^', '~', '\\', '&'));
            var resultMessage = parser.Parse(escapedResult);

            var r = resultMessage as MFR_M01;
            if (r == null)
                throw new InvalidOperationException(
                    $"Result was expected to be {typeof(NHapi.Model.V24.Message.MFR_M01)}, but was {resultMessage.GetType()}");

            Assert.IsTrue(r.ZvpRepetitionsUsed == 4);
        }

        [Test]
        public void ParseSingleZIS()
        {
            var result = @"MSH|^~\&|DIP|Blood Lab|LIS|XYZ Laboratory|20160818131019376||MFR^M01|VMSG14|P|2.4|
        MSA|AA|12345678|22237649|INFORM HPV HR Tissue|
        ZIS|1234^PR^PI|20160817082024472|Hemo|";

            var parser = new PipeParser();
            var escapedResult = Escape.unescape(result, new EncodingCharacters('|', '^', '~', '\\', '&'));
            var resultMessage = parser.Parse(escapedResult);

            var r = resultMessage as MFR_M01;
            if (r == null)
                throw new InvalidOperationException(
                    $"Result was expected to be {typeof(MFR_M01)}, but was {resultMessage.GetType()}");

            var zis = r.ZIS_GROUPS.First()
                .ZIS;

            Assert.AreEqual(zis.Identifier.Identifier.Value, "1234");
            Assert.AreEqual(zis.Identifier.Text.Value, "PR");
            Assert.AreEqual(zis.Identifier.NameOfCodingSystem.Value, "PI");
            Assert.AreEqual(zis.DateTimeOfCreation.TimeOfAnEvent.Value, "20160817082024472");
            Assert.AreEqual(zis.Procedure.Value, "Hemo");
        }
    }
}