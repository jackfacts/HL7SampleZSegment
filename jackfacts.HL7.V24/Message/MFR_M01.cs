using System;
using System.Collections.Generic;
using jackfacts.HL7.V24.Group;
using NHapi.Base;
using NHapi.Base.Log;
using NHapi.Base.Parser;

namespace jackfacts.HL7.V24.Message
{
    [Serializable]
    public class MFR_M01 : NHapi.Model.V24.Message.MFR_M01
    {
        public MFR_M01()
        {
            Init(new DefaultModelClassFactory());
        }

        public MFR_M01(IModelClassFactory factory) : base(factory)
        {
            Init(factory);
        }

        public IEnumerable<ZIS_GROUP> ZIS_GROUPS
        {
            get
            {
                for (var rep = 0; rep < ZvpRepetitionsUsed; rep++)
                    yield return (ZIS_GROUP) GetStructure(nameof(ZIS_GROUP), rep);
            }
        }

        /** 
         * Returns the number of existing repetitions of MFR_M01_MF_QUERY 
         */

        public int ZvpRepetitionsUsed
        {
            get
            {
                try
                {
                    return GetAll(nameof(ZIS_GROUP))
                        .Length;
                }
                catch (HL7Exception e)
                {
                    const string message =
                        "Unexpected error accessing data - this is probably a bug in the source code generator.";
                    HapiLogFactory.GetHapiLog(GetType())
                        .Error(message, e);
                    throw new Exception(message);
                }
            }
        }

        /// <summary>        /// initalize method for ADT_A08. This does the segment setup for the message.        ///</summary>
        private void Init(IModelClassFactory factory)
        {
            try
            {
                add(typeof(ZIS_GROUP), true, true);
            }
            catch (HL7Exception e)
            {
                HapiLogFactory.GetHapiLog(GetType())
                    .Error("Unexpected error creating ADT_A08 - this is probably a bug in the source code generator.", e);
            }
        }

        /// <summary>
        ///     Returns  first repetition of ZISGroup (a Group object) - creates it if necessary
        /// </summary>
        public ZIS_GROUP GetZISGroup()
        {
            try
            {
                return (ZIS_GROUP) GetStructure(nameof(ZIS_GROUP));
            }
            catch (HL7Exception e)
            {
                HapiLogFactory.GetHapiLog(GetType())
                    .Error("Unexpected error accessing data - this is probably a bug in the source code generator.", e);
                throw new Exception("An unexpected error ocurred", e);
            }
        }

        /// <summary>
        ///     Returns a specific repetition of ZISGroup
        ///     * (a Group object) - creates it if necessary
        ///     throws HL7Exception if the repetition requested is more than one
        ///     greater than the number of existing repetitions.
        /// </summary>
        public ZIS_GROUP GetZISGroup(int rep)
        {
            return (ZIS_GROUP) GetStructure(nameof(ZIS_GROUP), rep);
        }

        /// <summary>
        ///     Adds a new ZIS
        /// </summary>
        public ZIS_GROUP AddZIS()
        {
            return AddStructure(nameof(ZIS_GROUP)) as ZIS_GROUP;
        }

        /// <summary>
        ///     Removes the given ZIS
        /// </summary>
        public void RemoveZIS(ZIS_GROUP toRemove)
        {
            RemoveStructure(nameof(ZIS_GROUP), toRemove);
        }

        /// <summary>
        ///     Removes the ZIS at the given index
        /// </summary>
        public void RemoveZISAt(int index)
        {
            RemoveRepetition(nameof(ZIS_GROUP), index);
        }
    }
}