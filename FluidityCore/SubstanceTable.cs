using DataContractJsonSerializer;
using System;

namespace Fluidity.Core
{

    public enum State {GAS, LIQUID, SOLID};

    [DataContract]
    internal class Substance
    {
        [DataMember]
        int substanceId;
        [DataMember]
        string nameGas;
        [DataMember]
        string nameLiquid;
        [DataMember]
        string nameSolid;
        [DataMember]
        float boilingPoint;
        [DataMember]
        float meltingPoint;
        [DataMember]
        float molarMass;
        [DataMember]
        float heatCapacityGas;
        [DataMember]
        float heatCapacityLiquid;
        [DataMember]
        float heatCapacitySolid;
        [DataMember]
        float thermalConductivityGas;
        [DataMember]
        float thermalConductivityLiquid;
        [DataMember]
        float thermalConductivitySolid;
        [DataMember]
        float infraredAbsorptionGas;
        [DataMember]
        float infraredAbsorptionLiquid;
        [DataMember]
        float infraredAbsorptionSolid;
        [DataMember]
        float frictionCoeficientGas;
        [DataMember]
        float frictionCoeficientLiquid;
        [DataMember]
        float frictionCoeficientSolid;
        [DataMember]
        float blackBodyFactorGas;
        [DataMember]
        float blackBodyFactorLiquid;
        [DataMember]
        float blackBodyFactorSolid;
        [DataMember]
        float latentHeatVaporizing;
        [DataMember]
        float latentHeatMelting;
        [DataMember]
        float breakingStrenght;

        public Substance()
        {

        }
        
        public int getSubstanceId()
        {
            return substanceId;
        }
    }


    class SubstanceTable
    {
        private SubstanceTable substanceTable;
        int MAX_SUBSTANCES = 1024;
        Substance[] substances = new Substance[MAX_SUBSTANCES];

        public SubstanceTable(List<Substance> substanceList)
        {
            if (substanceList.lenght <= MAX_SUBSTANCES) {
                foreach (Substance substance in substanceList) {
                    substances[substance.getSubstanceId] = substance;
                }
            }
            //TODO: read JSON or other data
        }

        public static void setInstance(SubstanceTable instance)
        {
            substanceTable = instance;
        }

        public static SubstanceTable getInstance()
        {
            if (SubstanceTable != null)
            {
                return substanceTable;
            } else
            {
                Console.WriteLine("Error! SubstanceTable not initialized!");
            }
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

        public float getHeatcapacity(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].heatCapacityGas;
                case State.LIQUID: return substances[substanceId].heatCapacityLiquid;
                case State.SOLID: return substances[substanceId].heatCapacitySolid;
                default: return 0.0;
            }
        }

        public float getthermalConductivity(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].thermalConductivityGas;
                case State.LIQUID: return substances[substanceId].thermalConductivityLiquid;
                case State.SOLID: return substances[substanceId].thermalConductivitySolid;
                default: return 0.0;
            }
        }

        public float getinfraredAbsorption(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].infraredAbsorptionGas;
                case State.LIQUID: return substances[substanceId].infraredAbsorptionLiquid;
                case State.SOLID: return substances[substanceId].infraredAbsorptionSolid;
                default: return 0.0;
            }
        }

        public float getfrictionCoeficient(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].frictionCoeficientGas;
                case State.LIQUID: return substances[substanceId].frictionCoeficientLiquid;
                case State.SOLID: return substances[substanceId].frictionCoeficientSolid;
                default: return 0.0;
            }
        }

        public float getblackBodyFactor(State state, int substanceId)
        {
            switch (state)
            {
                case State.GAS: return substances[substanceId].blackBodyFactorGas;
                case State.LIQUID: return substances[substanceId].blackBodyFactorLiquid;
                case State.SOLID: return substances[substanceId].blackBodyFactorSolid;
                default: return 0.0;
            }
        }

        public float getLatentHeat(State state, int substanceId)
        {
            switch (state)
            {
                case State.LIQUID: return substances[substanceId].latentHeatVaporizing;
                case State.SOLID: return substances[substanceId].latentHeatMelting;
                default: return 0.0;
            }
        }

        public float getBoilingPointK(int substanceId)
        {
            return substances[substanceId].boilingPoint;
        }

        public float getMeltingPointK(int substanceId)
        {
            return substances[substanceId].meltingPoint;
        }

        public float getMolarMass(int substanceId)
        {
            return molarMass;
        }
    }
}