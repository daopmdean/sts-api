using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Data.Entities;
using Data.Models.Requests;
using Data.Models.Responses;
using Data.Pagings;
using Data.Repositories.Interfaces;
using Service.Exceptions;
using Service.Interfaces;

namespace Service.Implementations
{
    public class StoreStaffService : IStoreStaffService
    {
        private readonly IStoreStaffRepository _storeStaffRepo;
        private readonly IStoreRepository _storeRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public StoreStaffService(
            IStoreStaffRepository storeStaffRepo,
            IStoreRepository storeRepo,
            IUserRepository userRepo,
            IMapper mapper)
        {
            _storeStaffRepo = storeStaffRepo;
            _storeRepo = storeRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<StoreStaff> AssignStaff(StoreAssign assign)
        {
            var create = _mapper.Map<StoreStaffCreate>(assign);
            create.IsManager = false;
            create.IsPrimaryStore = false;

            return await CreateStoreStaff(create);
        }

        public async Task<StoreStaff> AssignStoreManager(StoreAssign assign)
        {
            var create = _mapper.Map<StoreStaffCreate>(assign);
            create.IsManager = true;
            create.IsPrimaryStore = true;

            var user = await _userRepo
                .GetUserByUsernameAsync(assign.Username);

            if (user.RoleId == (int)Enums.UserRole.Staff)
            {
                user.RoleId = (int)Enums.UserRole.StoreManager;
            }
            else if (user.RoleId != (int)Enums.UserRole.StoreManager)
            {
                throw new AppException((int)Enums.StatusCode.BadRequest,
                    "Can not assign due to user role");
            }

            return await CreateStoreStaff(create);
        }

        public async Task<StoreStaff> CreateStoreStaff(StoreStaffCreate create)
        {
            var store = await _storeRepo.GetByIdAsync(create.StoreId);

            if (store == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, StoreId does not exist");

            var user = await _userRepo.GetUserByUsernameAsync(create.Username);

            if (user == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, Username does not exist");

            if (store.BrandId != user.BrandId)
                throw new AppException(400,
                    "Can not assign Staff to Store outside Brand");

            var storeStaff = _mapper.Map<StoreStaff>(create);
            await _storeStaffRepo.CreateAsync(storeStaff);

            if (await _storeStaffRepo.SaveChangesAsync())
                return storeStaff;

            throw new AppException(400, "Can not create StoreStaff");
        }

        public async Task DeleteStoreStaff(int storeId, string username)
        {
            var storeStaff = await _storeStaffRepo
                .GetStoreStaffAsync(storeId, username);

            if (storeStaff == null)
                throw new AppException(400, "StoreStaff not found");

            _storeStaffRepo.Delete(storeStaff);

            if (await _storeStaffRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete StoreStaff");
        }

        public async Task<PagedList<StoreStaffOverview>> GetStaffFromStoreAsync(
            int storeId, StoreStaffParams @params)
        {
            return await _storeStaffRepo
                .GetStaffFromStoreAsync(storeId, @params);
        }

        public async Task<IEnumerable<StoreStaffOverview>> GetStaffFromStoreAsync(
            int storeId)
        {
            return await _storeStaffRepo
                .GetStaffFromStoreAsync(storeId);
        }

        public async Task<int> GetStaffStoreIdAsync(string username)
        {
            return await _storeStaffRepo
                .GetStaffStoreId(username);
        }

        public async Task<PagedList<StoreStaffOverview>> GetStoresFromStaffAsync(
            string username, StoreStaffParams @params)
        {
            return await _storeStaffRepo
                .GetStoresFromStaffAsync(username, @params);
        }

        public async Task<StoreStaff> GetStoreStaffAsync(int storeId,
            string username)
        {
            var storeStaff = await _storeStaffRepo
                .GetStoreStaffAsync(storeId, username);

            if (storeStaff == null)
                throw new AppException(400,
                    "StoreStaff not found or has been deleted");

            return storeStaff;
        }

        public async Task UpdateStoreStaff(int storeId,
            string username, StoreStaffUpdate update)
        {
            var storeStaff = await _storeStaffRepo
                .GetStoreStaffAsync(storeId, username);

            if (storeStaff == null)
                throw new AppException(400, "StoreStaff not found");

            _mapper.Map(update, storeStaff);

            _storeStaffRepo.Update(storeStaff);

            if (await _storeStaffRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update StoreStaff");
        }
    }
}
