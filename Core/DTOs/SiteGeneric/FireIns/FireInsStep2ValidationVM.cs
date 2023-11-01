using Microsoft.AspNetCore.Http;

namespace Core.DTOs.SiteGeneric.FireIns
{
    public class FireInsStep2ValidationVM
    {
        public FireInsStep2VM FireInsStep2VM { get; set; }
        public IFormFile PropertiesListFile { get; set; }
        public string Str_PropertiesListFile { get; set; }
        public IFormFile ExteriorofBuildingImage { get; set; }
        public string Str_ExteriorofBuildingImage { get; set; }
        public IFormFile InsuranceLocationInputImage { get; set; }
        public string Str_InsuranceLocationInputImage { get; set; }
        public IFormFile MainMeterandElectricalPanelImage { get; set; }
        public string Str_MainMeterandElectricalPanelImage { get; set; }
        public IFormFile InsuredPlaceFuseandMeterImage { get; set; }
        public string Str_InsuredPlaceFuseandMeterImage { get; set; }
        public IFormFile InsuredPlaceMeterandGasBranchesImage { get; set; }
        public string Str_InsuredPlaceMeterandGasBranchesImage { get; set; }
        public IFormFile GasBurningDevice1Image { get; set; }
        public string Str_GasBurningDevice1Image { get; set; }
        public IFormFile GasBurningDevice2Image { get; set; }
        public string Str_GasBurningDevice2Image { get; set; }
        public IFormFile GasBurningDevice3Image { get; set; }
        public string Str_GasBurningDevice3Image { get; set; }
        public IFormFile GasBurningDevice4Image { get; set; }
        public string Str_GasBurningDevice4Image { get; set; }
        public IFormFile WholeInteriorFilm { get; set; }
        public string Str_WholeInteriorFilm { get; set; }
    }
}
