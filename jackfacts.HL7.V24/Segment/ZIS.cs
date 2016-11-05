using System;
using NHapi.Base;
using NHapi.Base.Log;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V24.Datatype;
using NHapi.Model.V24.Segment;

namespace jackfacts.HL7.V24.Segment
{
    //ZIS|999^My Image 2^IMAGE|20151225122237649|Image Procedure|"
    [Serializable]
    public sealed class ZIS : Zxx
    {
        private const string DefaultErrorMessage = "Unexpected problem obtaining field value.  This is a bug.";

        private const string DefaultExceptionMessage = "An unexpected error ocurred";

        public ZIS(IGroup parent, IModelClassFactory factory) : base(parent, factory)
        {
            IMessage message = Message;

            try
            {
                add(typeof(CE),
                    true,
                    1,
                    250,
                    new object[]
                    {
                        message
                    },
                    "Idetifier");
                add(typeof(TS),
                    false,
                    1,
                    26,
                    new object[]
                    {
                        message
                    },
                    "DateTimeOfCreation");
                add(typeof(ST),
                    false,
                    1,
                    60,
                    new object[]
                    {
                        message
                    },
                    "Procedure");
            }
            catch (HL7Exception he)
            {
                HapiLogFactory.GetHapiLog(GetType())
                    .Error("Can't instantiate " + GetType()
                               .Name,
                        he);
            }
        }

        public CE StainIdentifier
        {
            get
            {
                try
                {
                    return (CE) GetField(1, 0);
                }
                catch (HL7Exception he)
                {
                    HapiLogFactory.GetHapiLog(GetType())
                        .Error(DefaultErrorMessage, he);
                    throw new Exception(DefaultExceptionMessage, he);
                }
                catch (Exception ex)
                {
                    HapiLogFactory.GetHapiLog(GetType())
                        .Error(DefaultErrorMessage, ex);
                    throw new Exception(DefaultExceptionMessage, ex);
                }
            }
        }

        public TS DateTimeOfCreation
        {
            get
            {
                try
                {
                    return (TS) GetField(2, 0);
                }
                catch (HL7Exception he)
                {
                    HapiLogFactory.GetHapiLog(GetType())
                        .Error(DefaultErrorMessage, he);
                    throw new Exception(DefaultExceptionMessage, he);
                }
                catch (Exception ex)
                {
                    HapiLogFactory.GetHapiLog(GetType())
                        .Error(DefaultErrorMessage, ex);
                    throw new Exception(DefaultExceptionMessage, ex);
                }
            }
        }

        public ST Procedure
        {
            get
            {
                try
                {
                    return (ST) GetField(3, 0);
                }
                catch (HL7Exception he)
                {
                    HapiLogFactory.GetHapiLog(GetType())
                        .Error(DefaultErrorMessage, he);
                    throw new Exception(DefaultExceptionMessage, he);
                }
                catch (Exception ex)
                {
                    HapiLogFactory.GetHapiLog(GetType())
                        .Error(DefaultErrorMessage, ex);
                    throw new Exception(DefaultExceptionMessage, ex);
                }
            }
        }
    }
}