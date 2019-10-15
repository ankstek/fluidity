using System.Runtime.Serialization;
using System.Collections.Generic;
using System;

namespace Fluidity.Core
{

    public enum State {GAS, LIQUID, SOLID};

    [DataContract]
    struct Substance
    {
        [DataMember]
        public int substanceId;
        [DataMember]
        public string nameGas;
        [DataMember]
        public string nameLiquid;
        [DataMember]
        public string nameSolid;
        [DataMember]
        public double boilingPoint;
        [DataMember]
        public double meltingPoint;
        [DataMember]
        public double molarMass;
        [DataMember]
        public double heatCapacityGas;
        [DataMember]
        public double heatCapacityLiquid;
        [DataMember]
        public double heatCapacitySolid;
        [DataMember]
        public double thermalConductivityGas;
        [DataMember]
        public double thermalConductivityLiquid;
        [DataMember]
        public double thermalConductivitySolid;
        [DataMember]
        public double infraredAbsorptionGas;
        [DataMember]
        public double infraredAbsorptionLiquid;
        [DataMember]
        public double infraredAbsorptionSolid;
        [DataMember]
        public double frictionCoeficientGas;
        [DataMember]
        public double frictionCoeficientLiquid;
        [DataMember]
        public double frictionCoeficientSolid;
        [DataMember]
        public double blackBodyFactorGas;
        [DataMember]
        public double blackBodyFactorLiquid;
        [DataMember]
        public double blackBodyFactorSolid;
        [DataMember]
        public double latentHeatVaporizing;
        [DataMember]
        public double latentHeatMelting;
        [DataMember]
        public double breakingStrenght;

        public int getSubstanceId() {
            return substanceId;
        }
    }


    class SubstanceTable
    {
        private SubstanceTable substanceTable;
        int MAX_SUBSTANCES = 1024;
        Substance[] substances;

        public SubstanceTable(List<Substance> substanceList)
        {
            substances = new Substance[MAX_SUBSTANCES];
            if (substanceList.Count <= MAX_SUBSTANCES) {
                foreach (Substance substance in substanceList) {
                    substances[substance.getSubstanceId()] = substance;
                }
            }
            //TODO: read JSON or other data
        }

        public string getName(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].nameGas;
                case State.LIQUID: return substances[substanceId].nameLiquid;
                case State.SOLID: return substances[substanceId].nameSolid;
                default: return "UNKNOWN_STATE";
            }
        }

        public double getHeatcapacity(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].heatCapacityGas;
                case State.LIQUID: return substances[substanceId].heatCapacityLiquid;
                case State.SOLID: return substances[substanceId].heatCapacitySolid;
                default: return 0.0;
            }
        }

        public double getthermalConductivity(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].thermalConductivityGas;
                case State.LIQUID: return substances[substanceId].thermalConductivityLiquid;
                case State.SOLID: return substances[substanceId].thermalConductivitySolid;
                default: return 0.0;
            }
        }

        public double getinfraredAbsorption(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].infraredAbsorptionGas;
                case State.LIQUID: return substances[substanceId].infraredAbsorptionLiquid;
                case State.SOLID: return substances[substanceId].infraredAbsorptionSolid;
                default: return 0.0;
            }
        }

        public double getfrictionCoeficient(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].frictionCoeficientGas;
                case State.LIQUID: return substances[substanceId].frictionCoeficientLiquid;
                case State.SOLID: return substances[substanceId].frictionCoeficientSolid;
                default: return 0.0;
            }
        }

        public double getblackBodyFactor(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].blackBodyFactorGas;
                case State.LIQUID: return substances[substanceId].blackBodyFactorLiquid;
                case State.SOLID: return substances[substanceId].blackBodyFactorSolid;
                default: return 0.0;
            }
        }

        public double getLatentHeat(State state, int substanceId)
        {
            switch (state)
            {
                case State.LIQUID: return substances[substanceId].latentHeatVaporizing;
                case State.SOLID: return substances[substanceId].latentHeatMelting;
                default: return 0.0;
            }
        }

        public double getBoilingPointK(int substanceId)
        {
            return substances[substanceId].boilingPoint;
        }

        public double getMeltingPointK(int substanceId)
        {
            return substances[substanceId].meltingPoint;
        }

        public double getMolarMass(int substanceId)
        {
            return substances[substanceId].molarMass;
        }
    }
}