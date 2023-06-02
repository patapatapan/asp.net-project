using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class vmGeneratorModel
{
    public string TypeNo { get; set; } = "";
    public vmMetadataModel MetadataModel { get; set; } = new vmMetadataModel();
    public vmRepositoryModel RepositoryModel { get; set; } = new vmRepositoryModel();
    public vmControllerModel ControllerModel { get; set; } = new vmControllerModel();
    public vmViewModel IndexViewModel { get; set; } = new vmViewModel();
    public vmViewModel CreateEditViewModel { get; set; } = new vmViewModel();
}