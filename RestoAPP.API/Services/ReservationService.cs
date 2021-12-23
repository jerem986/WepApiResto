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

        public ReservationService(RestoDbContext dc)
        {
            this.dc = dc;
        }

        public int AddReservation(ReservationAddDTO reservation)
        {
            if (reservation == null) throw new ArgumentNullException();
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
                        Client = new ClientDetailsDTO { 
                            Id = r.Client.Id
                        },
                        
                    });

            // mapping complet a la main et voir pour ajouter les emplacements

        }
    // intégration récupération des réservation en y ajoutant des critere
    // la date et le service seront requis --> logique car on regard sur quelle service on se positionne
    // le nom peut être aussi utiliser mais du coup on retournerai toute les reservations de cette personne... ce qui n'est pas une meilleurs idée autant y ajouter la date
    // est ce que la table est bonne en utlisant l'id du client ou faut-il incorporer directement le client et donc travailller avec un include ou un join?
    // pareil avec la table choisie

    //voir clientService et ClientController quelle est la meilleure tactique pour protéger les entrées/sortie dans la db ?
    //throw new execption? return bool a la place des void? 

    //voir pour aligner les éléements dans angular voir restaurant chez gusteau

    //changement icon dans la roulette cadre d'affichage?

    //carroussel wtf ??

    //prix https://wallux.com/festi-food ? tarif comment s'organiser?


    public bool DeleteById(int id)
        {
            Reservation reservation = dc.Reservation.FirstOrDefault(r => r.Id == id);
            if (reservation == null) return false;
            dc.Reservation.Remove(reservation);
            dc.SaveChanges();
            return true;
        }
        public bool Edit(ReservationDetailDTO reservation)
        {
            try
            {
                dc.Reservation.Update(reservation.MapTo<Reservation>());
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
    }
}
