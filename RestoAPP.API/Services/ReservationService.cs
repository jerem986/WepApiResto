using Microsoft.EntityFrameworkCore;
using RestoAPP.API.DTO.Client;
using RestoAPP.API.DTO.Reservations;
using RestoAPP.API.DTO.VBooking;
using RestoAPP.DAL;
using RestoAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToolBox.AutoMapper.Mappers;

namespace RestoAPP.API.Services
{
    public class ReservationService
    {
        private readonly RestoDbContext dc;
        private readonly ClientService _clientService;

        public ReservationService(RestoDbContext dc, ClientService clientService)
        {
            this.dc = dc;
            this._clientService = clientService;
        }

        public int AddReservation(ReservationAddDTO reservation)
        {
            if (reservation == null) throw new ArgumentNullException();
            reservation.DateDeRes = reservation.DateDeRes.AddDays(1); //ajout d'un jour suite a la reprise de la date avec le datepicker qui le met en GMT+1H et donc quand la date est reprise elle est enregistrée en GMT-1H ce qui fait basculer la date le jour avant
            Reservation tempReservation = new Reservation
            {
                ValidationStatuts = DAL.Enums.ValidationStatus.Pending,
                IdClient = reservation.IdClient,
                HeureReservation = reservation.Horaire,
                NbPers = reservation.NbPers,
                DateDeRes = reservation.DateDeRes,
            };
            dc.Reservation.Add(tempReservation);
            dc.SaveChanges();
            return tempReservation.Id;
        }

        public IEnumerable<ReservationDetailDTO> GetReservation(DateTime start, DateTime end)// intégration récupération des réservation en y ajoutant des critere
        {
            return dc.Reservation.Where(r => r.DateDeRes >= start && r.DateDeRes <= end)
                .Select(r => new ReservationDetailDTO { 
                    Id = r.Id,
                    DateDeRes = r.DateDeRes,
                    Horaire = r.HeureReservation,
                    IdClient = r.IdClient,
                    NbPers = r.NbPers,
                });

            // mapping complet a la main

        }


    public void DeleteById(int id)
        {
            Reservation reservation = dc.Reservation.FirstOrDefault(r => r.Id == id);
            if (reservation == null)
                throw new KeyNotFoundException();
            dc.Reservation.Remove(reservation);
            dc.SaveChanges();
        }
        public bool Edit(ReservationDetailDTO reservation)
        {
            try
            {
                //Reservation tempRes = new Reservation
                //{
                //    Id = reservation.Id,
                //    NbPers = reservation.NbPers,
                //    IdClient = reservation.IdClient,
                //    DateDeRes = reservation.DateDeRes,
                //    ValidationStatuts = reservation.ValidationStatuts,
                //    HeureReservation = reservation.Horaire
                //};

                //dc.Reservation.Update(tempRes);
                //dc.SaveChanges();

                Reservation tempRes = dc.Reservation.Find(reservation.Id);
                if (tempRes == null)
                    throw new KeyNotFoundException();
                tempRes.HeureReservation = reservation.Horaire;
                tempRes.DateDeRes = reservation.DateDeRes;
                tempRes.NbPers = reservation.NbPers;
                tempRes.ValidationStatuts = reservation.ValidationStatuts;
                tempRes.IdClient = reservation.IdClient;
                dc.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<VBookingDTO> GetByMonth(int month, int years)
        {
            return dc.VBooking.Where(x => x.DateDeRes.Year == years && x.DateDeRes.Month == month).MapToList<VBookingDTO>();
        }

        public IEnumerable<ReservationDetailDTO> GetReservationByIDClient(int id)
        {
            return dc.Reservation.Where(x => x.IdClient == id ).MapToList<ReservationDetailDTO>();
        }

        public IEnumerable<ReservationGestionDTO> GetReservationByDate(DateTime date)
        {
            return dc.Reservation.Include(i => i.Client).Select(m=> new ReservationGestionDTO
            {
                DateDeRes = m.DateDeRes,
                Horaire = m.HeureReservation,
                Id = m.Id,
                IdClient = m.IdClient,
                NbPers = m.NbPers,
                ValidationStatuts = m.ValidationStatuts,
                Name = m.Client.Name
            }).Where(x => x.DateDeRes == date).MapToList<ReservationGestionDTO>();
        }
    }
}
