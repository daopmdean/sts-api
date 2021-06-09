using System;
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
    public class ShiftAssignmentService : IShiftAssignmentService
    {
        private readonly IShiftAssignmentRepository _shiftAssignmentRepo;
        private readonly IUserRepository _userRepo;
        private readonly IStoreRepository _storeRepo;
        private readonly ISkillRepository _skillRepo;
        private readonly IMapper _mapper;

        public ShiftAssignmentService(
            IShiftAssignmentRepository shiftAssignmentRepo,
            IUserRepository userRepo,
            IStoreRepository storeRepo,
            ISkillRepository skillRepo,
            IMapper mapper)
        {
            _shiftAssignmentRepo = shiftAssignmentRepo;
            _userRepo = userRepo;
            _storeRepo = storeRepo;
            _skillRepo = skillRepo;
            _mapper = mapper;
        }

        public async Task<ShiftAssignment> CreateShiftAssignment(
            ShiftAssignmentCreate create)
        {
            var store = await _storeRepo
                .GetByIdAsync(create.StoreId);

            if (store == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, StoreId does not exist");

            var skill = await _skillRepo
                .GetByIdAsync(create.SkillId);

            if (skill == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, SkillId does not exist");

            var user = await _userRepo
                .GetUserByUsernameAsync(create.Username);

            if (user == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, Username does not exist");

            var shiftAssignment = _mapper.Map<ShiftAssignment>(create);
            await _shiftAssignmentRepo.CreateAsync(shiftAssignment);

            if (await _shiftAssignmentRepo.SaveChangesAsync())
                return shiftAssignment;

            throw new AppException(400, "Can not create ShiftAssignment");
        }

        public async Task DeleteShiftAssignment(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ShiftAssignment> GetShiftAssignment(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<ShiftAssignmentOverview>> GetShiftAssignments(string username, ShiftAssignmentParams @params)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateShiftAssignment(int id, ShiftAssignmentUpdate update)
        {
            throw new NotImplementedException();
        }
    }
}
