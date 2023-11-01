using Core.Services.Interfaces;
using DataLayer.Context;
using DataLayer.Entities.InsPolicy.ThirdParty;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class VehicleGroupService : IVehicleGroupService
    {
        private readonly MyContext _context;
        public VehicleGroupService(MyContext context)
        {
            _context = context;
        }
        public async Task<bool> AddToVehicleGroupVehicleUsages(int groupId, List<int> SelectedUsages)
        {
            try
            {
                VehicleGroup vehicleGroup = await _context.VehicleGroups.SingleOrDefaultAsync(x => x.Id == groupId);
                if (vehicleGroup == null)
                    return false;
                List<VehicleGroupUsage> vehicleGroupUsages = await _context.VehicleGroupUsages.Include(x => x.VehicleGroup).Include(x => x.VehicleUsage)
                                            .Where(w => w.VehicleGroup.Id == groupId).ToListAsync();
                List<VehicleUsage> CurUsages = vehicleGroupUsages.Select(x => x.VehicleUsage).ToList();
                List<int> CurUsageIds = CurUsages.Select(x => x.Id).ToList();
                List<int> Remove_Newusagess_Expect_CurUsageIds = CurUsageIds.Except(SelectedUsages).ToList(); //return items from permissions that not in perIds
                List<int> Add_CurusageIds_Expect_Newusages = SelectedUsages.Except(CurUsageIds).ToList(); //return items from perIds that not in permissions

                bool RemRes = await RemoveUsagesFromGroup(groupId, Remove_Newusagess_Expect_CurUsageIds);
                bool AddRes = await AddUsageToGroup(groupId, Add_CurusageIds_Expect_Newusages);

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string m = ex.Message;
                return false;
            }
        }
        private async Task<bool> AddUsageToGroup(int groupId, List<int> Usages)
        {
            List<VehicleGroupUsage> vehicleGroupUsages = new();
            foreach (int u in Usages)
            {
                vehicleGroupUsages.Add(new VehicleGroupUsage
                {
                    UsageId = u,
                    GroupId = groupId
                });

            }
            if (Usages.Count != 0)
            {
                await _context.VehicleGroupUsages.AddRangeAsync(vehicleGroupUsages);
            }
            return true;
        }
        public async Task<bool> RemoveUsagesFromGroup(int groupId, List<int> Usages)
        {
            List<VehicleGroupUsage> vehicleGroupUsages = new();
            foreach (int u in Usages)
            {
                vehicleGroupUsages.Add(new VehicleGroupUsage
                {
                    UsageId = u,
                    GroupId = groupId
                });

            }
            if (vehicleGroupUsages.Count != 0)
            {
                foreach (VehicleGroupUsage item in vehicleGroupUsages)
                {
                    VehicleGroupUsage remm = await _context.VehicleGroupUsages.SingleOrDefaultAsync(x => x.GroupId == groupId && x.UsageId == item.UsageId);
                    if (remm != null)
                    {
                        _context.VehicleGroupUsages.Remove(remm);
                    }

                }

            }
            return true;
        }

        public async Task<List<VehicleUsage>> GetVehicleUsages()
        {
            return await _context.VehicleUsages.Include(x => x.VehicleGroupUsages).ToListAsync();
        }

        public async Task<List<VehicleGroupUsage>> GetVehicleGroupUsagesByGroupIdAsync(int groupId)
        {
            return await _context.VehicleGroupUsages.Include(x => x.VehicleGroup).Include(x => x.VehicleUsage).Where(w => w.GroupId == groupId).ToListAsync();
        }

        public async Task<bool> UpdateVehicleGroupVehicleUsages(int groupId, List<int> SelectedUsages)
        {
            List<VehicleGroupUsage> vehicleGroupUsages = await _context.VehicleGroupUsages.Where(w => w.GroupId == groupId).ToListAsync();
            List<int> CurUsageIds = vehicleGroupUsages.Select(x => x.UsageId.Value).ToList();
            List<int> Diff1 = CurUsageIds.Except(SelectedUsages).ToList();
            List<int> Diff2 = SelectedUsages.Except(CurUsageIds).ToList();
            foreach (int item in Diff1)
            {
                VehicleGroupUsage gu = await _context.VehicleGroupUsages.FirstOrDefaultAsync(f => f.GroupId == groupId && f.UsageId == item);
                if (gu != null)
                {
                    _context.VehicleGroupUsages.Remove(gu);                    
                }

            }
            foreach (int item in Diff2)
            {
                VehicleGroupUsage vehicleGroupUsage = new() { GroupId = groupId, UsageId = item };
                _context.VehicleGroupUsages.Add(vehicleGroupUsage);               

            }           
            return true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<VehicleGroup>> GetVehicleGroupsAsync()
        {
            return await _context.VehicleGroups.Include(x => x.Parent).Include(x => x.VehicleGroupUsages).ToListAsync();
        }

        public async Task<VehicleGroup> GetVehicleGroupById(int id)
        {
            return await _context.VehicleGroups.Include(x => x.Parent).Include(x => x.VehicleGroupUsages).SingleOrDefaultAsync(x => x.Id == id);
        }

        public void CreateVehicleGroup(VehicleGroup vehicleGroup)
        {
            _context.VehicleGroups.Add(vehicleGroup);
        }

        public void UpdateVehicleGroup(VehicleGroup vehicleGroup)
        {
            _context.Update(vehicleGroup);
        }

        public async Task DeleteVehicleGroup(int id)
        {
            List<VehicleGroupUsage> vehicleGroupUsages = await _context.VehicleGroupUsages.Where(w => w.GroupId == id).ToListAsync();
            VehicleGroup vg = await _context.VehicleGroups.Include(x => x.Parent).SingleOrDefaultAsync(x => x.Id == id);
            List<VehicleGroup> vehicleGroups = await _context.VehicleGroups.Where(w => w.ParentId == vg.Id).Include(x => x.Parent).ToListAsync();
            foreach (VehicleGroupUsage vehicleGroupUsage in vehicleGroupUsages)
            {
                _context.VehicleGroupUsages.Remove(vehicleGroupUsage);
                
            }
            foreach (var item in vehicleGroups)
            {
                _context.VehicleGroups.Remove(item);
            }
            _context.VehicleGroups.Remove(vg);
        }
    }
}
