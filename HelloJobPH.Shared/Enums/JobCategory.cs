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

        [Display(Name = "Other")]
        Other
    }
}
