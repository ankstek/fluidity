using System;
namespace Fluidity.Core
{
    enum TemperatureScale {KELVIN, CELSIUS, FARENHEIT };

    class Tools
    {
        public static readonly double GAS_CONSTANT = 8.31446261815324;
        public static readonly double R_THROUGH_CV = 1.31950791077289; //precalculated for when volume is halved

        public static double getDisplayTemperature(double temperatureK, TemperatureScale playerTempScale)
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
        public static double getPressure(int substanceId, double temperatureK, double mass, int subtileResolution) {
            //joule / kubicmeter
            return getAmount(substanceId, mass) * GAS_CONSTANT * temperatureK / getSubtileVolume(subtileResolution);
        }

        public static double getFriction(int substanceId)
        {
            //TODO
            return 0;
        }

        public static double getSubtileArea(int subtileResolution) {
            switch (subtileResolution)
            {
                case 1: return 4;
                case 2: return 2;
                case 4: return 1;
                case 8: return 0.5;
                default: return 4 / subtileResolution;
            }
        }

        public static double getSubtileVolume(int subtileResolution) {
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

        public static double getSubstanceMixTemperature(int substanceId, double mass1, double mass2, double temperature1, double temperature2) 
        {
            double n1 = getAmount(substanceId, mass1);
            double n2 = getAmount(substanceId, mass2);
            return (n1 * temperature1) + (n2 * temperature2) / (n1 + n2);
        }

        public static double getSubstanceMixPressure(double substanceDensity, int subtileResolution, double mass1, double mass2, double temperature1, double temperature2)
        {
            double n1 = getAmount(substanceDensity, mass1);
            double n2 = getAmount(substanceDensity, mass2);
            return GAS_CONSTANT * ((n1 * temperature1) + (n2 * temperature2)) / (2 * getSubtileVolume(subtileResolution));
        }

        public static double getCompressedTemperature(double temperature)
        {
            return R_THROUGH_CV * temperature;
        }

        public static double getCompressedPressure(double pressure)
        {
            return pressure * 2 * R_THROUGH_CV;
        }

        public static double getExpandedTemperature(double temperature)
        {
            //TODO
            return temperature;
        }

        public static double getExpandedPressure(double pressure)
        {
            //TODO
            return pressure;
        }

        public static double getAmount(double substanceDensity, double mass) {
            return (mass * 1000) / substanceDensity;
        }

        public static double getSpecificBlackbodyRadiation(double temperatureK, double subtileArea, double blackbodyFactor) {
            return getGetBlackbodyRadiation(temperatureK, subtileArea) * blackbodyFactor;
        }

        public static double getGetBlackbodyRadiation(double temperatureK, double subtileArea)
        {
            return Math.Pow(temperatureK, 4) * (5.67 / 10000000) * subtileArea;
        }

        public double getLatentHeat(int substanceId, double mass) {
            //TODO
            return 0.0;
        }

        public static double getVaporPressure()
        {
            //TODO
            return 0.0;
        }

        public static double getTemperatureDelta(double mass, double specificHeatCapacity, double thermalEnergy)
        {
            return thermalEnergy / (specificHeatCapacity * mass); 
        }

        //momentum: p = m*v
        //KE: E = m * v^2 * 1/2
        // E = p^2 / 2m
    }
}