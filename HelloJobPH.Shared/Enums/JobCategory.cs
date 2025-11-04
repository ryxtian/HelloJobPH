using System;
using System.ComponentModel.DataAnnotations;

namespace HelloJobPH.Shared.Enums
{
    public enum JobCategory
    {
        [Display(Name = "Information & Communication Technology")]
        InformationAndCommunicationTechnology,

        [Display(Name = "Manufacturing, Transport & Logistics")]
        ManufacturingTransportAndLogistics,

        [Display(Name = "Retail & Consumer Products")]
        RetailAndConsumerProducts,

        [Display(Name = "Banking & Financial Services")]
        BankingAndFinancialServices,

        [Display(Name = "Construction")]
        Construction,

        [Display(Name = "Marketing & Communications")]
        MarketingAndCommunications,

        [Display(Name = "Human Resources & Recruitment")]
        HumanResourcesAndRecruitment,

        [Display(Name = "Administration & Office Support")]
        AdministrationAndOfficeSupport,

        [Display(Name = "Engineering")]
        Engineering,

        [Display(Name = "Education")]
        Education,

        [Display(Name = "Healthcare & Medical")]
        HealthcareAndMedical,

        [Display(Name = "Hospitality & Tourism")]
        HospitalityAndTourism,

        [Display(Name = "Sales")]
        Sales,

        [Display(Name = "Legal & Compliance")]
        LegalAndCompliance,

        [Display(Name = "Creative & Design")]
        CreativeAndDesign,

        [Display(Name = "Science & Research")]
        ScienceAndResearch,

        [Display(Name = "Government & Public Sector")]
        GovernmentAndPublicSector,

        [Display(Name = "Energy & Utilities")]
        EnergyAndUtilities,

        [Display(Name = "Real Estate & Property")]
        RealEstateAndProperty,

        [Display(Name = "Telecommunications")]
        Telecommunications,

        [Display(Name = "Customer Service & Support")]
        CustomerServiceAndSupport,

        [Display(Name = "Supply Chain & Procurement")]
        SupplyChainAndProcurement,

        [Display(Name = "Insurance")]
        Insurance,

        [Display(Name = "Media & Entertainment")]
        MediaAndEntertainment,

        [Display(Name = "Agriculture & Farming")]
        AgricultureAndFarming,

        [Display(Name = "Transportation & Logistics")]
        TransportationAndLogistics,

        [Display(Name = "Other")]
        Other
    }

}
