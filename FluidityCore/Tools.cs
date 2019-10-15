using Math;
namespace Fluidity.Core
{
    enum TemperatureScale {KELVIN, CELSIUS, FARENHEIT };

    class Tools
    {
        public static readonly float GAS_CONSTANT = 8.31446261815324;
        public static readonly float R_THROUGH_CV = 1.31950791077289; //precalculated for when volume is halved

        public static float getDisplayTemperature(float temperatureK, TemperatureScale playerTempScale)
        {
            switch (playerTempScale)
            {
                case TemperatureScale.KELVIN: return temperatureK;
                case TemperatureScale.CELSIUS: return temperatureK - 272.15;
                case TemperatureScale.FARENHEIT: return temperatureK * (1.8) - 459.67;
                default: return temperatureK;
            }
        }

        /*
         * This is simplified to the ideal gas law, this is because having each individual gas real compressability
         * would be a nightmare, and I consider this close enough
         */
        public static float getPressure(int substanceId, float temperatureK, float mass, int subtileResolution) {
            //joule / kubicmeter
            return getAmount(substanceId, mass) * GAS_CONSTANT * temperatureK / getSubtileVolume(subtileResolution);
        }

        public static float getFriction(int substanceId)
        {
            //TODO
            return 0;
        }

        public static float getSubtileArea(int subtileResolution) {
            switch (subtileResolution)
            {
                case 1: return 4;
                case 2: return 2;
                case 4: return 1;
                case 8: return 0.5;
                default: return 4 / subtileResolution;
            }
        }

        public static float getSubtileVolume(int subtileResolution) {
            //subtileResolution is 1,2,4 or 8. I don't think it should go any higher but the option is there...
            //kubic meter
            switch (subtileResolution)
            {
                case 1: return 1;
                case 2: return 0.25;
                case 4: return 0.0625;
                case 8: return 0.015625;
                default:
                    return 1/(subtileResolution * subtileResolution);
            }
        }

        public static float getSubstanceMixTemperature(int substanceId, float mass1, float mass2, float temperature1, float temperature2) 
        {
            float n1 = getAmount(substanceId, mass1);
            float n2 = getAmount(substanceId, mass2);
            return (n1 * temperature1) + (n2 * temperature2) / (n1 + n2);
        }

        public static float getSubstanceMixPressure(float substanceDensity, int subtileResolution, float mass1, float mass2, float temperature1, float temperature2)
        {
            float n1 = getAmount(substanceDensity, mass1);
            float n2 = getAmount(substanceDensity, mass2);
            return GAS_CONSTANT * ((n1 * temperature1) + (n2 * temperature2)) / (2 * getSubtileVolume(subtileResolution));
        }

        public static float getCompressedTemperature(float temperature)
        {
            return R_THROUGH_CV * temperature;
        }

        public static float getCompressedPressure(float pressure)
        {
            return pressure * 2 * R_THROUGH_CV;
        }

        public static float getExpandedTemperature(float temperature)
        {
            //TODO
            return temperature;
        }

        public static float getExpandedPressure(float pressure)
        {
            //TODO
            return pressure;
        }

        public static float getAmount(float substanceDensity, float mass) {
            return (mass * 1000) / substanceDensity;
        }

        public static float getSpecificBlackbodyRadiation(float temperatureK, float subtileArea, float blackbodyFactor) {
            return getGetBlackbodyRadiation(temperatureK, subtileArea) * blackbodyFactor;
        }

        public static float getGetBlackbodyRadiation(float temperatureK, float subtileArea)
        {
            return Math.Pow(temperatureK, 4) * (5.67 / 10000000) * subtileArea;
        }

        public float getLatentHeat(int substanceId, float mass) {
            //TODO
            return 0.0;
        }

        public static float getVaporPressure()
        {
            //TODO
            return 0.0;
        }

        public static float getTemperatureDelta(float mass, float specificHeatCapacity, float thermalEnergy)
        {
            return thermalEnergy / (specificHeatCapacity * mass); 
        }

        //momentum: p = m*v
        //KE: E = m * v^2 * 1/2
        // E = p^2 / 2m
    }
}