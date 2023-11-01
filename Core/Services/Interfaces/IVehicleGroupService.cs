using DataLayer.Entities.InsPolicy.ThirdParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IVehicleGroupService
    {
        void SaveChanges();
        Task SaveChangesAsync();
        Task<List<VehicleGroup>> GetVehicleGroupsAsync();
        Task<VehicleGroup> GetVehicleGroupById(int id);
        void CreateVehicleGroup(VehicleGroup vehicleGroup);
        void UpdateVehicleGroup(VehicleGroup vehicleGroup);
        Task DeleteVehicleGroup(int id);
        
        Task<bool> AddToVehicleGroupVehicleUsages(int groupId, List<int> SelectedUsages);
        Task<bool> UpdateVehicleGroupVehicleUsages(int groupId, List<int> SelectedUsages);
        Task<List<VehicleUsage>> GetVehicleUsages();
        Task<List<VehicleGroupUsage>> GetVehicleGroupUsagesByGroupIdAsync(int groupId);
    }
}
