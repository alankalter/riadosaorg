using RiadosaOrg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace RiadosaOrg.Controllers.API
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MovieFriendsController : ApiController
    {
        
        public IEnumerable<object> GetCurrentListings()
        {
            IEnumerable<Listing> Listings;
            using (var data = new revocarr_RiadosaOrgEntities())
            {
                Listings = data.Database.SqlQuery<Listing>(@"SELECT TOP (1000) 
		                                    --e.[Id]
                                              e.[Title]
                                              --,e.[MFLocationId]
                                              ,e.[URL]
                                              ,e.[AuxField]
                                              ,e.[Date]
                                              ,e.[Time]
                                              --,e.[DateTimeEntered]
                                              --,e.[Current]
	                                          ,l.Name as Location
                                          FROM [revocarr_RiadosaOrg].[revocarr_1].[MFEvent] e

                                          JOIN [revocarr_RiadosaOrg].[revocarr_1].[MFLocation] L
                                          on E.MFLocationId = L.Id

                                          WHERE e.[Current] = 1
                                          order by l.Name").ToList();
            }

            var listingsByLocation = new Dictionary<string, List<Listing>>();
            foreach (var listing in Listings)
            {
                if (!listingsByLocation.ContainsKey(listing.Location))
                    listingsByLocation.Add(listing.Location, new List<Listing> { listing });
                else
                {
                    listingsByLocation[listing.Location].Add(listing);
                }
            }

            var listingObjects = listingsByLocation.Select(x => new { name = x.Key, films = x.Value });
            return listingObjects;

        }
    }
}
