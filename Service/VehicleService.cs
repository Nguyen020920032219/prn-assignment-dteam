using System;
using Repository;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Entities;

namespace Service
{
    public class VehicleService
    {
        VehicleRepository _vehicleRepo;
        ValidationService _validationService;
        public VehicleService()
        {
            _vehicleRepo = new VehicleRepository();
            _validationService = new ValidationService();
        }

        public List<Vehicle> GetVehiclesByCustomerId(int customerId)
        {
            List<Vehicle> vehicles = _vehicleRepo.GetAll();
            List<Vehicle> result = new List<Vehicle>();
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.CustomerId == customerId)
                {
                    result.Add(vehicle);
                }
            }
            return result;
        }

        public List<Vehicle> GetVehiclesContainString(string txtSearch)
        {
            List<Vehicle> vehicles = _vehicleRepo.GetAll();
            List<Vehicle> result = new List<Vehicle>();
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.LicensePlate.ToLower().Contains(txtSearch.ToLower()))
                {
                    result.Add(vehicle);
                }
                if (vehicle.Make.ToLower().Contains(txtSearch.ToLower()))
                {
                    if (!result.Contains(vehicle))
                    {
                        result.Add(vehicle);
                    }
                }
                if (vehicle.Model.ToLower().Contains(txtSearch.ToLower()))
                {
                    if (!result.Contains(vehicle))
                    {
                        result.Add(vehicle);
                    }
                }
                    if (vehicle.Year.ToString().Contains(txtSearch))
                    {
                        if (!result.Contains(vehicle))
                        {
                            result.Add(vehicle);
                        }
                    }
            }
            return result;
        }

        public List<Vehicle> GetVehicles()
        {
            return _vehicleRepo.GetAll();
        }

        public void DeleteVehicle(Vehicle vehicle)
        {
            _vehicleRepo.Delete(vehicle);
        }

        public void AddVehicle(Vehicle vehicle)
        {
            _vehicleRepo.Add(vehicle);
        }
    }
}
