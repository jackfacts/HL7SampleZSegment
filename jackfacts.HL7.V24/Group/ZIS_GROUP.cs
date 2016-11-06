using System;
using jackfacts.HL7.V24.Segment;
using NHapi.Base;
using NHapi.Base.Log;
using NHapi.Base.Model;
using NHapi.Base.Parser;

namespace jackfacts.HL7.V24.Group
{
    [Serializable]
    public class ZIS_GROUP : AbstractGroup
    {
        public ZIS_GROUP(IGroup parent, IModelClassFactory factory) : base(parent, factory)
        {
            try
            {
                add(typeof(ZIS), false, false);
            }
            catch (HL7Exception e)
            {
                HapiLogFactory.GetHapiLog(GetType())
                    .Error(
                        "Unexpected error creating MFR_M01_MF_QUERY - this is probably a bug in the source code generator.",
                        e);
            }
        }

        /// <summary>
        ///     Returns ZIS (any Z segment) - creates it if necessary
        /// </summary>
        public ZIS ZIS
        {
            get
            {
                try
                {
                    return (ZIS) GetStructure(nameof(ZIS));
                }
                catch (HL7Exception e)
                {
                    HapiLogFactory.GetHapiLog(GetType())
                        .Error(
                            "Unexpected error accessing data - this is probably a bug in the source code generator.", e);
                    throw new Exception("An unexpected error ocurred", e);
                }
            }
        }
    }
}