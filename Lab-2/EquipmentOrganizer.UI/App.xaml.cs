using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EquipmentOrganizer.Model;
using EquipmentOrganizer.UI.ViewModels;

namespace EquipmentOrganizer.UI
{
    public partial class App : Application
    {
        private DataModel _model;
        private DataViewModel _viewModel;

        public void OnStartup(object sender, StartupEventArgs e)
        {
            var mapperConfigMtoV = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataModel, DataViewModel>();
                cfg.CreateMap<Equipment, EquipmentViewModel>();
                cfg.CreateMap<Employee, EmployeeViewModel>();
            });
            var mapperMtoV = mapperConfigMtoV.CreateMapper();
            _model = DataModel.Load();
            _viewModel = mapperMtoV.Map<DataModel, DataViewModel>(_model);

            var window = new MainWindow() { DataContext = _viewModel };
            window.Show();
        }

        protected override void OnExit(ExitEventArgs eventArgs)
        {
            try
            {
                var mapperConfigVtoM = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DataViewModel, DataModel>();
                    cfg.CreateMap<EquipmentViewModel, Equipment>();
                    cfg.CreateMap<EmployeeViewModel, Employee>();
                });
                var mapperVtoM = mapperConfigVtoM.CreateMapper();
                _model = mapperVtoM.Map<DataViewModel, DataModel>(_viewModel);
                _model.Save();
            }
            catch (Exception)
            {
                base.OnExit(null);
                throw;
            }

        }
    }
}
