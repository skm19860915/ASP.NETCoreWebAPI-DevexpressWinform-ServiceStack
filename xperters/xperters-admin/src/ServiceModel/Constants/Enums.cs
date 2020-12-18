using ServiceStack.DataAnnotations;

namespace Xperters.Admin.ServiceModel.Constants
{
    public static class Enums
    {
        [EnumAsInt]
        public enum CounterpartyType
        {
            NotSet = 0,
            FloridaDomestic = 1,
            MultiRegionalCarrier = 2,
            NationwideCarrier = 3,
            StatePool = 4,
            InternationalExUS = 5,
            GlobalWorldwide = 6,
            Regional = 7
        }

        [EnumAsInt]
        public enum Currency
        {
            NotSet = 0,
            AUD = 1,
            CAD = 2,
            CHF = 3,
            CNY = 4,
            EUR = 5,
            GBP = 6,
            INDEX = 7,
            INR = 8,
            JPY = 9,
            LKR = 10,
            MXN = 11,
            NOK = 12,
            NZD = 13,
            PHP = 14,
            RMB = 15,
            USD = 16,
            AED = 17,
            AFN = 18,
            ALL = 19,
            AMD = 20,
            AOA = 21,
            ARS = 22,
            AWG = 23,
            AZN = 24,
            BAM = 25,
            BBD = 26,
            BDT = 27,
            BGN = 28,
            BHD = 29,
            BIF = 30,
            BMD = 31,
            BND = 32,
            BOB = 33,
            BRL = 34,
            BSD = 35,
            BTN = 36,
            BWP = 37,
            BYR = 38,
            BZD = 39,
            CDF = 40,
            CLP = 41,
            COP = 42,
            CRC = 43,
            CUP = 44,
            CVE = 45,
            CZK = 46,
            DJF = 47,
            DKK = 48,
            DOP = 49,
            DZD = 50,
            EGP = 51,
            ERN = 52,
            ETB = 53,
            FJD = 54,
            FKP = 55,
            GEL = 56,
            GHS = 57,
            GIP = 58,
            GMD = 59,
            GNF = 60,
            GTQ = 61,
            GYD = 62,
            HKD = 63,
            HNL = 64,
            HRK = 65,
            HTG = 66,
            HUF = 67,
            IDR = 68,
            ILS = 69,
            IQD = 70,
            IRR = 71,
            ISK = 72,
            JMD = 73,
            JOD = 74,
            KES = 75,
            KGS = 76,
            KHR = 77,
            KPW = 78,
            KRW = 79,
            KWD = 80,
            KYD = 81,
            KZT = 82,
            LAK = 83,
            LBP = 84,
            LRD = 85,
            LSL = 86,
            LYD = 87,
            MAD = 88,
            MDL = 89,
            MGA = 90,
            MKD = 91,
            MMK = 92,
            MNT = 93,
            MOP = 94,
            MRO = 95,
            MUR = 96,
            MVR = 97,
            MWK = 98,
            MYR = 99,
            MZN = 100,
            NAD = 101,
            NGN = 102,
            NIO = 103,
            NPR = 104,
            OMR = 105,
            PAB = 106,
            PEN = 107,
            PGK = 108,
            PKR = 109,
            PLN = 110,
            PYG = 111,
            QAR = 112,
            RON = 113,
            RSD = 114,
            RUB = 115,
            RWF = 116,
            SAR = 117,
            SBD = 118,
            SCR = 119,
            SDG = 120,
            SEK = 121,
            SGD = 122,
            SHP = 123,
            SLL = 124,
            SOS = 125,
            SRD = 126,
            STD = 127,
            SYP = 128,
            SZL = 129,
            THB = 130,
            TJS = 131,
            TMT = 132,
            TND = 133,
            TOP = 134,
            TRY = 135,
            TTD = 136,
            TWD = 137,
            TZS = 138,
            UAH = 139,
            UGX = 140,
            UYU = 141,
            UZS = 142,
            VEF = 143,
            VND = 144,
            VUV = 145,
            WST = 146,
            XAF = 147,
            XCD = 148,
            XPF = 149,
            YER = 150,
            ZAR = 151,
            ZMW = 152,
            ZWL = 153
        }

        [EnumAsInt]
        public enum YesNoNa
        {
            NotSet = 0,
            No = 1,
            Yes = 2,
            NotApplicable = 3
        }

        [EnumAsInt]
        public enum YesNo
        {
            NotSet = 0,
            No = 1,
            Yes = 2
        }

        [EnumAsInt]
        public enum ClimateQuotaSharePercentage
        {
            NotSet = 0,
            NinetyPercent = 1,
            OneHundredPercent = 2,
            Other = 3
        }

        [EnumAsInt]
        public enum ClimateTradeStatus
        {
            NotSet = 0,
            Inquiry = 1,
            Pending = 2,
            Submitted = 3,
            Agreed = 4,
            Confirmed = 5,
            Deleted = 6,
            Expired = 7
        }

        [EnumAsInt]
        public enum StandardCalcOtherFlag
        {
            NotSet = 0,
            StandardCalc = 1,
            Other = 2
        }

        [EnumAsInt]
        public enum StandardOtherFlag
        {
            NotSet = 0,
            Standard = 1,
            Other = 2
        }

        [EnumAsInt]
        public enum CommodityExposure
        {
            NotSet = 0,
            HighPowerPrice = 1,
            LowPowerPrice = 2,
            HighCropYield = 5,
            LowCropYield = 6,
            HighNaturalGasPrice = 9,
            LowNaturalGasPrice = 10,
            HighOilPrice = 13,
            LowOilPrice = 14,
            PowerPriceNoDirection = 17,
            CropYieldNoDirection = 18,
            NaturalGasPriceNoDirection = 19,
            OilPriceNoDirection = 20
        }

        [EnumAsInt]
        public enum ContractSlipStatus
        {
            NotSet = 0,
            WaitingForDraft = 1,
            NCLReviewingDraft = 2,
            FrontReviewingDraft = 3,
            BrokerMarketReviewingDraft = 4,
            CountersignedDocsRequired = 5,
            FullyExecuted = 6
        }

        [EnumAsInt]
        public enum WeatherExposure
        {
            NotSet = 0,
            HighTemperature = 1,
            LowTemperature = 2,
            DualTemperature = 3,
            NormalTemperature = 4,
            HighRain = 5,
            LowRain = 6,
            HighSnow = 7,
            LowSnow = 8,
            HighIrradiance = 9,
            LowIrradiance = 10,
            HighWind = 11,
            LowWind = 12,
            TemperatureNoDirection = 13,
            RainNoDirection = 14,
            SnowNoDirection = 15,
            IrradianceNoDirection = 16,
            WindNoDirection = 17
        }

        [EnumAsInt]
        public enum CapitalProvider
        {
            NotSet = 0,
            FundManagementGroup = 1
        }

        [EnumAsInt]
        public enum Fund
        {
            NotSet = 0,
            ART = 1
        }

        [EnumAsInt]
        public enum EventCoverage
        {
            NotSet = 0,
            Aggregate = 1,
            First = 2,
            Second = 3,
            Third = 4,
            Fourth = 5,
            Fifth = 6,
            Sixth = 7,
            Seventh = 8,
            Eighth = 9,
            Ninth = 10
        }

        [EnumAsInt]
        public enum PayoutStructure
        {
            NotSet = 0,
            Proportional = 1,
            Binary = 2,
            Window = 3,
            Corridor = 4,
            ReverseCorridor = 5
        }

        [EnumAsInt]
        public enum CascadingStructure
        {
            NotSet = 0,
            Yes = 1,
            No = 2,
            NotApplicable = 3
        }

        [EnumAsInt]
        public enum CoverageBasis
        {
            NotSet = 0,
            LOD = 1,
            RAD = 2,
            CMD = 3
        }

        [EnumAsInt]
        public enum OccurrenceDeductableType
        {
            NotSet = 0,
            Franchise = 1,
            Traditional = 2,
            NotApplicable = 3
        }

        [EnumAsInt]
        public enum TriggerBasis
        {
            NotSet = 0,
            Aggregate = 1,
            Occurrence = 2,
            Cascading = 3
        }

        [EnumAsInt]
        public enum LayerType
        {
            NotSet = 0,
            ReinsuranceExcessOfLoss = 1,
            ReinsuranceProportional = 2,
            Retro = 3,
            ILW = 4,
            CatBond = 5,
            CWIL = 6,
            CapitalTrades = 7,
            Insurance = 8,
            Other = 9,
            WeatherDerivative = 10,
            ProxyRevenueSwap = 11,
            ClimateReinsurance = 12
        }

        [EnumAsInt]
        public enum TransactionFormat
        {
            NotSet = 0,
            Exchange = 1,
            ReinsuranceContract = 2,
            Security = 3,
            Swap = 4,
            Security144A = 5,
            Security4a2 = 6,
            CatBondLite = 7,
            PrivatePlacement = 8,
            BankLoan = 9,
            Derivative = 10,
            DebtOrEquity = 11
        }

        [EnumAsInt]
        public enum TrustStatus
        {
            NotSet = 0,
            Formation = 1,
            Completed = 2,
            Expired = 3,
            Closed = 4
        }

        [EnumAsInt]
        public enum TrustFundType
        {
            NotSet = 0,
            Gross = 1,
            Net = 2
        }

        [EnumAsInt]
        public enum IndexProviderEventValuationUpdateType
        {
            NotSet = 0,
            Preliminary = 1,
            ReSurvey = 2,
            Final = 3
        }

        [EnumAsInt]
        public enum ExceedanceProbabilityCurveType
        {
            NotSet = 0,
            Proforma = 1,
            Actual = 2
        }

        [EnumAsInt]
        public enum MarkStatus
        {
            NotSet = 0,
            Provisional = 1,
            Finalized = 2
        }

        [EnumAsInt]
        public enum LossStatus
        {
            NotSet = 0,
            Open = 1,
            Closed = 2,
            Commuted = 3
        }

        [EnumAsInt]
        public enum Country
        {
            NotSet = 0,
            AFG = 1,
            ALA = 2,
            ALB = 3,
            DZA = 4,
            ASM = 5,
            AND = 6,
            AGO = 7,
            AIA = 8,
            ATG = 9,
            ARG = 10,
            ARM = 11,
            ABW = 12,
            AUS = 13,
            AUT = 14,
            AZE = 15,
            BHS = 16,
            BHR = 17,
            BGD = 18,
            BRB = 19,
            BLR = 20,
            BEL = 21,
            BLZ = 22,
            BEN = 23,
            BMU = 24,
            BTN = 25,
            BOL = 26,
            BIH = 27,
            BWA = 28,
            BRA = 29,
            VGB = 30,
            BRN = 31,
            BGR = 32,
            BFA = 33,
            BDI = 34,
            KHM = 35,
            CMR = 36,
            CAN = 37,
            CPV = 38,
            CYM = 39,
            CAF = 40,
            TCD = 41,
            CHL = 42,
            CHN = 43,
            HKG = 44,
            MAC = 45,
            COL = 46,
            COM = 47,
            COG = 48,
            COK = 49,
            CRI = 50,
            CIV = 51,
            HRV = 52,
            CUB = 53,
            CYP = 54,
            CZE = 55,
            PRK = 56,
            COD = 57,
            DNK = 58,
            DJI = 59,
            DMA = 60,
            DOM = 61,
            ECU = 62,
            EGY = 63,
            SLV = 64,
            GNQ = 65,
            ERI = 66,
            EST = 67,
            ETH = 68,
            FRO = 69,
            FLK = 70,
            FJI = 71,
            FIN = 72,
            FRA = 73,
            GUF = 74,
            PYF = 75,
            GAB = 76,
            GMB = 77,
            GEO = 78,
            DEU = 79,
            GHA = 80,
            GIB = 81,
            GRC = 82,
            GRL = 83,
            GRD = 84,
            GLP = 85,
            GUM = 86,
            GTM = 87,
            GGY = 88,
            GIN = 89,
            GNB = 90,
            GUY = 91,
            HTI = 92,
            VAT = 93,
            HND = 94,
            HUN = 95,
            ISL = 96,
            IND = 97,
            IDN = 98,
            IRN = 99,
            IRQ = 100,
            IRL = 101,
            IMN = 102,
            ISR = 103,
            ITA = 104,
            JAM = 105,
            JPN = 106,
            JEY = 107,
            JOR = 108,
            KAZ = 109,
            KEN = 110,
            KIR = 111,
            KWT = 112,
            KGZ = 113,
            LAO = 114,
            LVA = 115,
            LBN = 116,
            LSO = 117,
            LBR = 118,
            LBY = 119,
            LIE = 120,
            LTU = 121,
            LUX = 122,
            MDG = 123,
            MWI = 124,
            MYS = 125,
            MDV = 126,
            MLI = 127,
            MLT = 128,
            MHL = 129,
            MTQ = 130,
            MRT = 131,
            MUS = 132,
            MYT = 133,
            MEX = 134,
            FSM = 135,
            MDA = 136,
            MCO = 137,
            MNG = 138,
            MNE = 139,
            MSR = 140,
            MAR = 141,
            MOZ = 142,
            MMR = 143,
            NAM = 144,
            NRU = 145,
            NPL = 146,
            NLD = 147,
            ANT = 148,
            NCL = 149,
            NZL = 150,
            NIC = 151,
            NER = 152,
            NGA = 153,
            NIU = 154,
            NFK = 155,
            MNP = 156,
            NOR = 157,
            PSE = 158,
            OMN = 159,
            PAK = 160,
            PLW = 161,
            PAN = 162,
            PNG = 163,
            PRY = 164,
            PER = 165,
            PHL = 166,
            PCN = 167,
            POL = 168,
            PRT = 169,
            PRI = 170,
            QAT = 171,
            KOR = 172,
            REU = 173,
            ROU = 174,
            RUS = 175,
            RWA = 176,
            BLM = 177,
            SHN = 178,
            KNA = 179,
            LCA = 180,
            MAF = 181,
            SPM = 182,
            VCT = 183,
            WSM = 184,
            SMR = 185,
            STP = 186,
            SAU = 187,
            SEN = 188,
            SRB = 189,
            SYC = 190,
            SLE = 191,
            SGP = 192,
            SVK = 193,
            SVN = 194,
            SLB = 195,
            SOM = 196,
            ZAF = 197,
            ESP = 198,
            LKA = 199,
            SDN = 200,
            SUR = 201,
            SJM = 202,
            SWZ = 203,
            SWE = 204,
            CHE = 205,
            SYR = 206,
            TJK = 207,
            THA = 208,
            MKD = 209,
            TLS = 210,
            TGO = 211,
            TKL = 212,
            TON = 213,
            TTO = 214,
            TUN = 215,
            TUR = 216,
            TKM = 217,
            TCA = 218,
            TUV = 219,
            UGA = 220,
            UKR = 221,
            ARE = 222,
            UK = 223,
            TZA = 224,
            USA = 225,
            VIR = 226,
            URY = 227,
            UZB = 228,
            VUT = 229,
            VEN = 230,
            VNM = 231,
            WLF = 232,
            ESH = 233,
            YEM = 234,
            ZMB = 235,
            ZWE = 236
        }

        [EnumAsInt]
        public enum PremiumScheduleFrequency
        {
            NotSet = 0,
            Monthly = 1,
            Quarterly = 2,
            SemiAnnually = 3,
            Annually = 4
        }

        [EnumAsInt]
        public enum PerspectiveType
        {
            NotSet = 0,
            Vendor = 1,
            Mercle = 2
        }

        [EnumAsInt]
        public enum WildfireCoverageType
        {
            NotSet = 0,
            NoCoverage = 1,
            CoveredNoCAExposure = 2,
            CoveredCAExposure = 3
        }

        [EnumAsInt]
        public enum Trustee
        {
            NotSet = 0,
            BONY = 1,
            Wilmington = 2,
            WellsFargo = 3,
            Comerica = 4
        }

        [EnumAsInt]
        public enum ClimateReinsuranceContractType
        {
            NotSet = 0,
            QuotaShare = 1,
            StopLoss = 2
        }

        [EnumAsInt]
        public enum ClimateRenewableMarket
        {
            NotSet = 0,
            ErcotNorth = 1,
            ErcotWest = 2,
            ErcotSouth = 3,
            ErcotHouston = 4,
            SppNorth = 5,
            SppSouth = 6,
            AustraliaNSW = 11,
            AustraliaVIC = 12,
            AustraliaSA = 13,
            AustraliaQLD = 14,
            AseoAlberta = 20,
            PjmNorthernIllinois = 21,
            PjmAepDayton = 22,
            PjmNewJersey = 23,
            PjmWestern = 24,
            PjmEastern = 25,
            PjmDominion = 26,
            PjmDom = 27,
            MisoArkansas = 31,
            MisoIllinois = 32,
            MisoIndiana = 33,
            MisoLouisiana = 34,
            MisoMichigan = 35,
            MisoMinn = 36,
            MisoTexas = 37,
            NyisoZoneAWest = 41,
            NyisoZoneBGenesse = 42,
            NyisoZoneCCentral = 43,
            NyisoZoneDNorth = 44,
            NyisoZoneEMohawkValley = 45,
            NyisoZoneFCapital = 46,
            NyisoZoneGHudsonValley = 47,
            NyisoZoneHMillwood = 48,
            NyisoZoneIDunwoodie = 49,
            NyisoZoneJNewYorkCity = 50,
            NyisoZoneKLongIsland = 51,
            IsoneMaine = 61,
            IsoneVermonth = 62,
            IsoneNewHampshire = 63,
            IsoneWesternCentralMass = 64,
            IsoneConnecticut = 65,
            IsoneNortheastMass = 66,
            IsoneSoutheastMass = 67,
            IsoneRhodeIsland = 68,
            DenmarkDK1 = 71,
            DenmarkDK2 = 72,
            NorwayNO1 = 73,
            NorwayNO2 = 74,
            NorwayNO3 = 75,
            NorwayNO4 = 76,
            NorwayNO5 = 77,
            SwedenSE1 = 78,
            SwedenSE2 = 79,
            SwedenSE3 = 80,
            SwedenSE4 = 81,
            ItalyITPUN = 82,
            ItalyITCNOR = 83,
            ItalyITCSUD = 84,
            ItalyITNORD = 85,
            ItalyITSARD = 86,
            ItalyITSICI = 87,
            ItalyITSUD = 88,
            FinlandFI = 101,
            RomaniaROU = 102,
            SpainESP = 103,
            GermanyDE = 104,
            NetherlandsNL = 105,
            GreatBritainGB = 106,
            FranceFR = 107,
            EstoniaEE = 108,
            LatviaLV = 109,
            LithuaniaLT = 110,
            AustriaAT = 111,
            BelgiumBE = 112,
            NordpoolSystemPrice = 199,
            Other = 999
        }

        [EnumAsInt]
        public enum ClimateRenewableStructurePricing
        {
            NotSet = 0,
            FixedAnnualPayment = 1,
            AnnualPremium = 2,
            OneTimesPremium = 3,
            OneTimesAndAnnualPremium = 4,
            PowerPurchaseAgreementRate = 5,
            ProxyGenerationPowerPurchaseAgreementRate = 6,
            FixedVolumeQuantity = 7,
            FixedVolumePrice = 8
        }

        [EnumAsInt]
        public enum ClimateRenewableComboStructure
        {
            NotSet = 0,
            NotApplicable = 1,
            BalanceOfHedge = 2,
            VolumeFirmingAgreement = 3,
            HedgedProxyRevenueSwap = 4
        }

        [EnumAsInt]
        public enum ClimateRenewableStructure
        {
            NotSet = 0,
            FixedVolumeSwap = 1,
            ProxyGenerationPowerPurchaseAgreement = 2,
            ProxyGenerationSwap = 3,
            ProxyRevenueSwap = 4
        }

        [EnumAsInt]
        public enum ClimateRenewableAssetType
        {
            NotSet = 0,
            Solar = 1,
            Wind = 2,
            Hydro = 3
        }

        [EnumAsInt]
        public enum ImpactCategory
        {
            NotSet = 0,
            NotApplicable = 1,
            TransitioningPopulations = 2,
            PublicEntities = 3,
            SustainableTechnologies = 4
        }

        [EnumAsInt]
        public enum WeatherDerivativeKind
        {
            NotSet = 0,
            Vanilla = 1,
            Quanto = 2
        }

        [EnumAsInt]
        public enum DerivativeOptionType
        {
            NotSet = 0,
            Call = 1,
            Put = 2,
            Swap = 3,
            Collar = 4,
            Strangle = 5,
            Straddle = 6
        }

        [EnumAsInt]
        public enum ClimateRenewableMarketSettlement
        {
            NotSet = 0,
            DayAhead = 1,
            RealTime = 2
        }

        [EnumAsInt]
        public enum PortfolioClimateStatus
        {
            NotSet = 0,
            Normal = 1,
            Active = 2,
            Inactive = 3
        }

        [EnumAsInt]
        public enum TerritoryExposure
        {
            NotSet = 0,
            SingleState = 1,
            Regional = 2,
            Nationwide = 3,
            InternationalExUS = 4,
            GlobalWorldwide = 5
        }

        [EnumAsInt]
        public enum BrokerageType
        {
            NotSet = 0,
            Applicable = 1,
            NotApplicable = 2,
            Reverse = 3
        }

        [EnumAsInt]
        public enum LineOfBusiness
        {
            NotSet = 0,
            Commercial = 1,
            Residential = 2,
            Auto = 3,
            MobileHomes = 4,
            Others = 5,
            SmallMediumBusiness = 6
        }

        [EnumAsInt]
        public enum PayReceivePremiumFlag
        {
            NotSet = 0,
            Pay = 1,
            Receive = 2
        }

        [EnumAsInt]
        public enum BuySellFlag
        {
            NotSet = 0,
            Buy = 1,
            Sell = 2
        }

        [EnumAsInt]
        public enum AnalysisApproach
        {
            NotSet = 0,
            Alpha = 1,
            Beta = 2
        }

        [EnumAsInt]
        public enum ActivationStatus
        {
            NotSet = 0,
            Activated = 1,
            Deleted = 2
        }

        [EnumAsInt]
        public enum AdjustmentMechanism
        {
            NotSet = 0,
            EL = 1,
            OneIn100 = 2,
            OneIn250 = 3,
            SubjectPremium = 4,
            TIV = 5,
            NotApplicable = 6
        }

        [EnumAsInt]
        public enum PostBoundDealStatus
        {
            NotSet = 0,
            CapacityRequested = 1,
            CapacityLocked = 2,
            Bound = 3,
            CapacityNotTakenUp = 4,
            CapacityNotInterested = 5,
            Expired = 6
        }

        [EnumAsInt]
        public enum LayerStatus
        {
            NotSet = 0,
            ForInfoOnly = 1,
            SubmissionReceived = 2,
            Planning = 3,
            AnalysisInProgress = 4,
            OnHoldAwaitingMoreInfo = 5,
            QuotedInvestmentAllocationCommitteeDecisionRequired = 6,
            QuotedInvestmentAllocationCommitteeDecisionCompleted = 7,
            AwaitingFirmOrderTerm = 8,
            AuthorizedInvestmentAllocationCommitteeDecisionRequired = 9,
            AuthorizedInvestmentAllocationCommitteeDecisionCompleted = 10,
            FinalizedBound = 11,
            FinalizedNotTakenUp = 12,
            FinalizedNotInterested = 13,
            Expired = 14,
            Deleted = 15,
            AnalysisInProgressUnderwriter = 16,
            ForModellingOnly = 17
        }

        [EnumAsInt]
        public enum BusinessType
        {
            NotSet = 0,
            Reinsurance = 1,
            Insurance = 2,
            Climate = 3
        }

        [EnumAsInt]
        public enum CapitalStatus
        {
            NotSet = 0,
            CapacityRequested = 1,
            CapacityLocked = 2,
            Bound = 3,
            CapacityNotTakenUp = 4,
            CapacityNotInterested = 5,
            Expired = 6,
            Deleted = 7
        }

        [EnumAsInt]
        public enum LayerLegExportStatus
        {
            NotSet = 0,
            ShouldExport = 1,
            ShouldNotExport = 2,
            Exporting = 3,
            ExportSucceeded = 4,
            ExportFailed = 5
        }

        [EnumAsInt]
        public enum TradeStatus
        {
            NotSet = 0,
            Expected = 1,
            Quoted = 2,
            Authorized = 3,
            Signed = 4
        }

        [EnumAsInt]
        public enum PlanningCategory
        {
            NotSet = 0,
            HighFL = 1,
            HighIntl = 2,
            HighSEWxFLW = 3,
            HighUSAny = 4,
            HighUSDiver = 5,
            LowFL = 6,
            LowIntl = 7,
            LowSEWxFLW = 8,
            LowUSAny = 9,
            LowUSDiver = 10,
            MediumFL = 11,
            MediumIntl = 12,
            MediumSEWxFLW = 13,
            MediumUSAny = 14,
            MediumUSDiver = 15,
            LowOther = 16,
            MediumOther = 17,
            HighOther = 18
        }

        [EnumAsInt]
        public enum LegacyDealStatus
        {
            NotSet = 0,
            ForInfoOnly = 1,
            Plan = 2,
            QuoteRequested = 3,
            Quoted = 4,
            Pending = 5,
            Authorized = 6,
            Submitted = 7,
            Finalized = 8,
            FutureInForce = 9,
            Agreed = 10,
            Confirmed = 11,
            PreNotTakenUp = 12,
            NotTakenUpOk = 13,
            NotTakenUpUnknown = 14,
            PreDeclined = 15,
            WillNotQuote = 16,
            DeclinedOk = 17,
            DeclinedUnknown = 18,
            Expired = 19,
            Deleted = 20,
            Duplicate = 21
        }

        [EnumAsInt]
        public enum SignOffStatus
        {
            NotSet = 0,
            SignOffRequired = 1,
            ReSignOffRequired = 2,
            SignedOff = 3
        }

        [EnumAsInt]
        public enum LegacyDealType
        {
            NotSet = 0,
            Reinsurance = 1,
            StopLoss = 2,
            StopLossReinsurance = 3,
            LloydsReinsurance = 4,
            LloydsReinsurance2016 = 5,
            QuotaShare = 6,
            QuoteShareReins = 7,
            Retro = 9,
            LloydsRetro2016 = 10,
            ILW = 11,
            LloydsILW2016 = 12,
            Catbond = 14,
            PrivateCatNote = 15,
            CWIP = 21,
            LloydsCWIP = 22,
            LloydsCWIP2016 = 23,
            Debt = 24,
            Equity = 25,
            AnankeShareClass = 26,
            Derivative = 27,
            Fee = 28,
            FXForward = 29,
            FXForwardCollateralized = 30,
            FXSpot = 31,
            Life = 32,
            MarkToMarket = 33,
            StudentLoan = 34,
            LloydsILW = 36,
            LloydsRetro = 37,
            LloydsILWBuy = 38,
            Weather = 39
        }

        [EnumAsInt]
        public enum ExternalSystem
        {
            NotSet = 0,
            Carburetor = 1,
            Ignition = 2,
            Zephyr = 3,
            Calypso = 4,
            Dms = 5,
            Robomodeler = 6
        }

        [EnumAsInt]
        public enum MultiYearLimitType
        {
            NotSet = 0,
            AnnualReset = 1,
            NonAnnualReset = 2
        }

        [EnumAsInt]
        public enum EarningCurveCode
        {
            NotSet = 0,
            AustraliaNZAllPerils = 1,
            AustraliaNZQuake = 2,
            CAQuake = 3,
            EUAllPerils = 4,
            EUAllPerilsJPAllPerils = 5,
            EUQuake = 6,
            EUWind = 7,
            EUWindJapanWind = 8,
            JapanAllPerils = 9,
            JapanQuake = 10,
            JapanTyphoon = 11,
            NotDefined = 12,
            Other = 13,
            StraightUSAllNaturalPerilsFromGroundUpCommercial = 14,
            StraightUSAllNaturalPerilsFromGroundUpHomeowners = 15,
            USAllPerils = 16,
            USAllPerilsFromGroundUp = 17,
            USAllPerilsEUAllPerils = 18,
            USAllPerilsEUWind = 19,
            USAllPerilsJapanAllPerils = 20,
            USAllPerilsWorldwide = 21,
            USQuake = 22,
            USWind = 23,
            USWindFromGroundUp = 24,
            USWindEUWind = 25,
            USWindJapanAllPerils = 26,
            Weather = 27,
            Worldwide = 28,
            WorldwideQuake = 29,
            USWindJapanQuake = 30,
            WeatherDerivativeCumulativeDailyPayout = 31,
            WeatherDerivativeTenYearBurn = 32,
            ProxyRevenueSwap = 33,
            CropIndia = 34,
            ClimateManualTracker = 35,
            CropIndiaRabi = 36,
            CropIndiaKharif = 37,
            CropChina = 38,
            CropUsaHemp = 39,
            ForestryChina = 40,
            LivestockChina = 41,
            ClimateCombinedChina = 42
        }

        [EnumAsInt]
        public enum FlatOverrideTailFeeType
        {
            NotSet = 0,
            Gross = 1,
            Net = 2,
            NotApplicable = 3
        }

        [EnumAsInt]
        public enum DirectOrReinsuranceIndicator
        {
            NotSet = 0,
            InwardDirect = 1,
            InwardProportionalTreaty = 2,
            InwardNonProportionalTreaty = 3
        }

        [EnumAsInt]
        public enum RiskCode
        {
            NotSet = 0,
            VelocityBinder = 2,
            Treaty = 3,
            PropertyExcessOfLossAmerica = 4,
            PropertyExcessOfLossJapan = 5,
            PropertyExcessOfLossRestOfWorld = 6,
            PropertyExcessOfLossEurope = 7,
            PropertyExcessOfLossRetrocession = 8,
            Cyber = 9,
            BoilerAndMachinery = 10,
            OverseasLegTerrorismProperty = 11,
            DifferenceInCondition = 12,
            Weather = 13,
            WeatherAgriculture = 14
        }

        [EnumAsInt]
        public enum TaxPremiumType
        {
            NotSet = 0,
            Gross = 1,
            Net = 2,
            NotApplicable = 3,
            Reverse = 4
        }

        [EnumAsInt]
        public enum PremiumType
        {
            NotSet = 0,
            Gross = 1,
            Net = 2,
            NotApplicable = 3
        }

        [EnumAsInt]
        public enum MarketAvailability
        {
            NotSet = 0,
            Private = 1,
            Club = 2,
            Syndicated = 3
        }

        [EnumAsInt]
        public enum ExternalReportingDealClassification
        {
            NotSet = 0,
            Reinsurance = 1,
            Retro = 2,
            ILW = 3,
            CatBond = 4,
            CWIP = 5,
            UpstreamReinsurance = 6,
            UpstreamDirect = 7,
            UpstreamAllPerils = 8,
            UpstreamCatOnly = 9,
            UpstreamDebtAndEquity = 10,
            UpstreamAllPerilsDirect = 11,
            Other = 12,
            Internal = 13,
            Climate = 14
        }

        [EnumAsInt]
        public enum IndexProvider
        {
            NotSet = 0,
            PCS = 1,
            MunichReNatCat = 2,
            SwissReSigma = 3,
            PerilsAG = 4,
            NotApplicable = 5,
            NPRA = 6,
            GCQuakeCube = 7,
            NOAA = 8
        }

        [EnumAsInt]
        public enum ExposureTrigger
        {
            NotSet = 0,
            Indemnity = 1,
            IndustryIndex = 2,
            IndustryIndexandIndemnity = 3,
            IndustryLoss = 4,
            ModelledLoss = 5,
            Parametric = 6,
            Various = 7
        }

        [EnumAsInt]
        public enum HurricaneStormSurgeCoverage
        {
            NotSet = 0,
            NotCovered = 1,
            CoveredAsFlood = 2,
            CoveredAsHurricaneWind = 3
        }

        [EnumAsInt]
        public enum TerrorismCoverage
        {
            NotSet = 0,
            NoExposure = 1,
            SilentExposure = 2,
            LowExposure = 3,
            HighExposure = 4
        }

        [EnumAsInt]
        public enum ClassOfBusiness
        {
            NotSet = 0,
            Indexed = 1,
            CatRI = 2,
            Weather = 3,
            MGA = 4
        }

        [EnumAsInt]
        public enum CyberCoverage
        {
            NotSet = 0,
            NoExposure = 1,
            SilentExposure = 2,
            LowExposure = 3,
            HighExposure = 4
        }

        [EnumAsInt]
        public enum AmBestRating
        {
            NotSet = 0,
            APlusPlus = 1,
            APlus = 2,
            A = 3,
            AMinus = 4,
            BPlusPlus = 5,
            BPlus = 6,
            B = 7,
            BMinus = 8,
            CPlusPlus = 9,
            CPlus = 10,
            C = 11,
            CMinus = 12,
            D = 13,
            E = 14,
            F = 15,
            S = 16,
            NotRated = 17
        }

        [EnumAsInt]
        public enum MoodysRating
        {
            NotSet = 0,
            AAA = 1,
            AA1 = 2,
            AA2 = 3,
            AA3 = 4,
            A1 = 5,
            A2 = 6,
            A3 = 7,
            BAA1 = 8,
            BAA2 = 9,
            BAA3 = 10,
            BA1 = 11,
            BA2 = 12,
            BA3 = 13,
            B1 = 14,
            B2 = 15,
            B3 = 16,
            CAA1 = 17,
            CAA2 = 18,
            CAA3 = 19,
            CA = 20,
            C = 21,
            NotRated = 22
        }

        [EnumAsInt]
        public enum StandardAndPoorsRating
        {
            NotSet = 0,
            AAA = 1,
            AAPlus = 2,
            AA = 3,
            AAMinus = 4,
            APlus = 5,
            A = 6,
            AMinus = 7,
            BBBPlus = 8,
            BBB = 9,
            BBBMinus = 10,
            BBPlus = 11,
            BB = 12,
            BBMinus = 13,
            BPlus = 14,
            B = 15,
            BMinus = 16,
            CCCPlus = 17,
            CCC = 18,
            CCCMinus = 19,
            CC = 20,
            C = 21,
            D = 22,
            NotRated = 23
        }

        [EnumAsInt]
        public enum CalculationAgent
        {
            NotSet = 0,
            ActualLosses = 1,
            RMS = 3,
            AIR = 4,
            Eqecat = 5,
            Milliman = 6,
            KatRisk = 7,
            Resurety = 8,
            WSP = 9
        }

        [EnumAsInt]
        public enum RiskModeler
        {
            NotSet = 0,
            RMS = 2,
            AIR = 3,
            Eqecat = 4,
            Milliman = 5,
            KatRisk = 6,
            NotApplicable = 7
        }

        [EnumAsInt]
        public enum RateType
        {
            NotSet = 0,
            OneMonthFed = 1,
            ThreeMonthLibor = 2,
            ThreeMonthTBill = 3,
            ThreeMonthEuribor = 4,
            SixMonthLibor = 5,
            TMM = 6
        }

        [EnumAsInt]
        public enum PolicyHistoryDescription
        {
            NotSet = 0,
            New = 1,
            Renewed = 2
        }

        [EnumAsInt]
        public enum ExposureLocation
        {
            NotSet = 0,
            QN = 1,
            QM = 2,
            QU = 3,
            US = 4,
            AU = 5,
            GB = 6,
            DE = 7,
            FR = 8,
            ES = 9,
            JP = 10,
            KR = 11,
            BM = 12,
            NZ = 13,
            IN = 14,
            LK = 15,
            SG = 16,
            AD = 17,
            AE = 18,
            AF = 19,
            AG = 20,
            AI = 21,
            AL = 22,
            AM = 23,
            AO = 24,
            AR = 25,
            AS = 26,
            AT = 27,
            AW = 28,
            AZ = 29,
            BA = 30,
            BB = 31,
            BD = 32,
            BE = 33,
            BF = 34,
            BG = 35,
            BH = 36,
            BI = 37,
            BJ = 38,
            BN = 39,
            BO = 40,
            BQ = 41,
            BR = 42,
            BS = 43,
            BT = 44,
            BW = 45,
            BY = 46,
            BZ = 47,
            CA = 48,
            CC = 49,
            CD = 50,
            CF = 51,
            CG = 52,
            CH = 53,
            CI = 54,
            CK = 55,
            CL = 56,
            CM = 57,
            CN = 58,
            CO = 59,
            CR = 60,
            CU = 61,
            CV = 62,
            CW = 63,
            CX = 64,
            CY = 65,
            CZ = 66,
            DJ = 67,
            DK = 68,
            DM = 69,
            DO = 70,
            DZ = 71,
            EC = 72,
            EE = 73,
            EG = 74,
            ER = 75,
            ET = 76,
            FI = 77,
            FJ = 78,
            FK = 79,
            FM = 80,
            FO = 81,
            GA = 82,
            GD = 83,
            GE = 84,
            GF = 85,
            GH = 86,
            GI = 87,
            GL = 88,
            GM = 89,
            GN = 90,
            GP = 91,
            GQ = 92,
            GR = 93,
            GT = 94,
            GU = 95,
            GW = 96,
            GY = 97,
            HK = 98,
            HN = 99,
            HR = 100,
            HT = 101,
            HU = 102,
            ID = 103,
            IE = 104,
            IL = 105,
            IM = 106,
            IO = 107,
            IQ = 108,
            IR = 109,
            IS = 110,
            IT = 111,
            JM = 112,
            JO = 113,
            KE = 114,
            KG = 115,
            KH = 116,
            KI = 117,
            KM = 118,
            KN = 119,
            KP = 120,
            KW = 121,
            KY = 122,
            KZ = 123,
            LA = 124,
            LB = 125,
            LC = 126,
            LI = 127,
            LR = 128,
            LS = 129,
            LT = 130,
            LU = 131,
            LV = 132,
            LY = 133,
            MA = 134,
            MC = 135,
            MD = 136,
            ME = 137,
            MG = 138,
            MH = 139,
            MK = 140,
            ML = 141,
            MM = 142,
            MN = 143,
            MO = 144,
            MP = 145,
            MQ = 146,
            MR = 147,
            MS = 148,
            MT = 149,
            MU = 150,
            MV = 151,
            MW = 152,
            MX = 153,
            MY = 154,
            MZ = 155,
            NA = 156,
            NC = 157,
            NE = 158,
            NF = 159,
            NG = 160,
            NI = 161,
            NL = 162,
            NO = 163,
            NP = 164,
            NR = 165,
            NU = 166,
            OM = 167,
            PA = 168,
            PE = 169,
            PF = 170,
            PG = 171,
            PH = 172,
            PK = 173,
            PL = 174,
            PM = 175,
            PR = 176,
            PS = 177,
            PT = 178,
            PW = 179,
            PY = 180,
            QA = 181,
            RE = 182,
            RO = 183,
            RS = 184,
            RU = 185,
            RW = 186,
            SA = 187,
            SB = 188,
            SC = 189,
            SD = 190,
            SE = 191,
            SH = 192,
            SI = 193,
            SK = 194,
            SL = 195,
            SM = 196,
            SN = 197,
            SO = 198,
            SR = 199,
            SS = 200,
            ST = 201,
            SV = 202,
            SX = 203,
            SY = 204,
            SZ = 205,
            TC = 206,
            TD = 207,
            TG = 208,
            TH = 209,
            TJ = 210,
            TK = 211,
            TL = 212,
            TM = 213,
            TN = 214,
            TO = 215,
            TR = 216,
            TT = 217,
            TV = 218,
            TW = 219,
            TZ = 220,
            UA = 221,
            UG = 222,
            UM = 223,
            UY = 224,
            UZ = 225,
            VA = 226,
            VC = 227,
            VE = 228,
            VG = 229,
            VI = 230,
            VN = 231,
            VU = 232,
            WF = 233,
            WS = 234,
            YE = 235,
            YT = 236,
            ZA = 237,
            ZM = 238,
            ZW = 239,
            AQ = 240,
            BL = 241,
            BV = 242,
            EH = 243,
            GG = 244,
            GS = 245,
            HM = 246,
            JE = 247,
            MF = 248,
            PN = 249,
            SJ = 250,
            TF = 251
        }

        [EnumAsInt]
        public enum ContractualRegionPerilExposure
        {
            NotSet = 0,
            USSouthEastWindTXNC = 1,
            USNorthEastWindVAME = 2,
            USFLWind = 3,
            USGulfTXAL = 4,
            USSEAtlanticGANC = 5,
            USOther = 6,
            CAQuake = 7,
            NMQuake = 8,
            EuropeWindAndFlood = 9,
            EuropeQuake = 10,
            JapanWind = 11,
            JapanQuake = 12,
            CaribbeanWindAndQuake = 13,
            AuzOrNZAllPerils = 14,
            ROWWindAndQuake = 15,
            NthEventUS = 16,
            NthEventWWExUS = 17,
            Aviation = 18,
            GulfOfMexicoOffshore = 19,
            NeitherElementalPerilsOrWildfireDeal = 20,
            WWAllPerils = 21
        }

        [EnumAsInt]
        public enum TriggerRetentionField
        {
            NotSet = 0,
            AggregateRetention = 1,
            OccurrenceDeductible = 2,
            CascadingEffectiveRetention = 3
        }

        public static class Insurance
        {
            [EnumAsInt]
            public enum FactorType
            {
                NotSet = 0,
                YearEndPlanScalingFactor = 1,
                NextYearPlanScalingFactor = 2,
                PremiumInForce = 3,
                Model = 4,
                StateNationalInsuranceCompanyFactor = 5,
                ManagingGeneralAgent = 6,
                PremiumProjection = 7
            }

            [EnumAsInt]
            public enum PremiumType
            {
                NotSet = 0,
                PremiumInForce = 3,
                PremiumProjection = 7
            }

            [EnumAsInt]
            public enum ScalingFactorType
            {
                NotSet = 0,
                YearEndPlanScalingFactor = 1,
                NextYearPlanScalingFactor = 2
            }

            [EnumAsInt]
            public enum Fund
            {
                NotSet = 0,
                Multi = 1,
                Itasca = 2
            }

            [EnumAsInt]
            public enum InForce
            {
                NotSet = 0,
                InForce = 1,
                Projection = 2
            }

            [EnumAsInt]
            public enum Peril
            {
                NotSet = 0,
                AllPerils = 1,
                Earthquake = 2,
                Wind = 3,
                Wildfire = 4,
                AllOtherPerils = 5,
                NaturalPerils = 6,
                NamedStormAndEarthquake = 7,
                Other = 99
            }

            [EnumAsInt]
            public enum Product
            {
                NotSet = 0,
                AllPerils = 1,
                WindOnly = 2,
                LargeCommercial = 3,
                SmallMediumBusinesses = 4,
                Condo = 5,
                Personal = 6,
                Catastrophe = 7,
                SurplusShare = 8,
                Commercial = 9
            }

            [EnumAsInt]
            public enum ProductType
            {
                NotSet = 0,
                Admitted = 1,
                NonAdmitted = 2
            }

            [EnumAsInt]
            public enum State
            {
                NotSet = 0,
                Alabama = 1,
                Alaska = 2,
                Arizona = 3,
                Arkansas = 4,
                California = 5,
                Colorado = 6,
                Connecticut = 7,
                Delaware = 8,
                Florida = 9,
                Georgia = 10,
                Hawaii = 11,
                Idaho = 12,
                Illinois = 13,
                Indiana = 14,
                Iowa = 15,
                Kansas = 16,
                Kentucky = 17,
                Louisiana = 18,
                Maine = 19,
                Maryland = 20,
                Massachusetts = 21,
                Michigan = 22,
                Minnesota = 23,
                Mississippi = 24,
                Missouri = 25,
                Montana = 26,
                Nebraska = 27,
                Nevada = 28,
                NewHampshire = 29,
                NewJersey = 30,
                NewMexico = 31,
                NewYork = 32,
                NorthCarolina = 33,
                NorthDakota = 34,
                Ohio = 35,
                Oklahoma = 36,
                Oregon = 37,
                Pennsylvania = 38,
                RhodeIsland = 39,
                SouthCarolina = 40,
                SouthDakota = 41,
                Tennessee = 42,
                Texas = 43,
                Utah = 44,
                Vermont = 45,
                Virginia = 46,
                Washington = 47,
                WestVirginia = 48,
                Wisconsin = 49,
                Wyoming = 50,
                Multi = 51
            }

            [EnumAsInt]
            public enum Status
            {
                NotSet = 0,
                Active = 1,
                Inactive = 2,
                Deleted = 3
            }
        }
    }
}