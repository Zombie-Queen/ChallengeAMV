using AMV.TourReservation.Application.Dtos;
using AMV.TourReservation.Application.RepositoryInterfaces;
using AMV.TourReservation.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AMV.TourReservation.Application.Services
{
    public class ReservationManagerService
    {

        protected readonly IReservationRepository _reservationRepository;
        protected readonly ITourRepository _tourRepository;
        private readonly IMapper _mapper;

        public ReservationManagerService(IReservationRepository reservationRepository, ITourRepository tourRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _tourRepository = tourRepository;
            _mapper = mapper;
        }

        public async Task AddTourAsync(TourDto tourdto)
        {
            var tour = _mapper.Map<Tour>(tourdto);
            await _tourRepository.AddAsync(tour);
            await _tourRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<TourDto> GetTourAsync(int id)
        {
            var tour = await _tourRepository.GetByIdAsync(id);
            return _mapper.Map<TourDto>(tour);
        }

        public async Task<List<TourDto>> GetAllToursAsync()
        {
            var tours = await _tourRepository.GetAllAsync();
            return _mapper.Map<List<TourDto>>(tours);
        }

        public async Task AddReservationAsync(ReservationDto reservationdto)
        {
            var reservation = _mapper.Map<Reservation>(reservationdto);
            await _reservationRepository.AddAsync(reservation);
            await _reservationRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<List<ReservationDto>> GetAllReservationsAsync()
        {
            var reservations = await _reservationRepository.GetReservationsWithTour();

            return _mapper.Map<List<ReservationDto>>(reservations);
        }

        public async Task<List<ReservationDto>> GetReservationsByTourAsync(int idtour)
        {
            var reservations = await _reservationRepository.GetReservationsByTour(idtour);

            return _mapper.Map<List<ReservationDto>>(reservations);
        }

        public async Task DeleteReservationAsync(int id)
        {
            await _reservationRepository.DeleteAsync(id);
            await _reservationRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTourAsync(int id)
        {
            await _tourRepository.DeleteAsync(id);
            await _tourRepository.UnitOfWork.SaveChangesAsync();
        }

        public async Task<ReservationDto> GetReservationAsync(int id)
        {
            var tour = await _reservationRepository.GetByIdAsync(id);
            return _mapper.Map<ReservationDto>(tour);
        }
    }
}
