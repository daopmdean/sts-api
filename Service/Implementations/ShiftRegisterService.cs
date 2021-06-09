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
    public class ShiftRegisterService : IShiftRegisterService
    {
        private readonly IShiftRegisterRepository _shiftRegisterRepo;
        private readonly IWeekScheduleRepository _weekScheduleRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public ShiftRegisterService(
            IShiftRegisterRepository shiftRegisterRepo,
            IWeekScheduleRepository weekScheduleRepo,
            IUserRepository userRepo,
            IMapper mapper)
        {
            _shiftRegisterRepo = shiftRegisterRepo;
            _weekScheduleRepo = weekScheduleRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ShiftRegister> CreateShiftRegister(
            ShiftRegisterCreate create)
        {
            var weekSchedule = await _weekScheduleRepo
                .GetByIdAsync(create.WeekScheduleId);

            if (weekSchedule == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, WeekScheduleId does not exist");

            var user = await _userRepo
                .GetUserByUsernameAsync(create.Username);

            if (user == null)
                throw new AppException(400,
                    "Conflicted with the FOREIGN KEY constraint, Username does not exist");

            var shiftRegister = _mapper.Map<ShiftRegister>(create);
            await _shiftRegisterRepo.CreateAsync(shiftRegister);

            if (await _shiftRegisterRepo.SaveChangesAsync())
                return shiftRegister;

            throw new AppException(400, "Can not create ShiftRegister");
        }

        public async Task DeleteShiftRegister(int id)
        {
            var shiftRegister = await _shiftRegisterRepo
                .GetByIdAsync(id);

            if (shiftRegister == null)
                throw new AppException(400, "ShiftRegister not found");

            _shiftRegisterRepo.Delete(shiftRegister);

            if (await _shiftRegisterRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not delete ShiftRegister");
        }

        public async Task<ShiftRegister> GetShiftRegister(int id)
        {
            var shiftRegister = await _shiftRegisterRepo
                .GetByIdAsync(id);

            if (shiftRegister == null)
                throw new AppException(400,
                    "ShiftRegister not found or has been deleted");

            return shiftRegister;
        }

        public async Task<PagedList<ShiftRegisterOverview>> GetShiftRegisters(
            string username, ShiftRegisterParams @params)
        {
            return await _shiftRegisterRepo
                .GetShiftRegistersAsync(username, @params);
        }

        public async Task UpdateShiftRegister(int id, ShiftRegisterUpdate update)
        {
            var shiftRegister = await _shiftRegisterRepo
                .GetByIdAsync(id);

            if (shiftRegister == null)
                throw new AppException(400, "ShiftRegister not found");

            _mapper.Map(update, shiftRegister);

            _shiftRegisterRepo.Update(shiftRegister);

            if (await _shiftRegisterRepo.SaveChangesAsync())
                return;

            throw new AppException(400, "Can not update ShiftRegister");
        }
    }
}
