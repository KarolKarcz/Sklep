using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Entities.ProductEntities;

namespace API.Controllers.Helpers
{
    public static class ConvertDtosToEntities
    {
        internal static void ConvertUserAddress(ref AppUser user, UserAddressDto addressDto)
        {
            user.PersonalData.Adress.City = addressDto.City;
            user.PersonalData.Adress.CompanyName = addressDto.CompanyName;
            user.PersonalData.Adress.Nip = addressDto.Nip;
            user.PersonalData.Adress.PostalCode = addressDto.PostalCode;
            user.PersonalData.Adress.StreetAndHouseNumber = addressDto.StreetAndHouseNumber;
            user.PersonalData.Adress.TelephoneNumber = addressDto.TelephoneNumber;
        }

        internal static void ConvertUserNameAndSurname(ref AppUser user, NameAndSurnameDto nameAndSurnameDto)
        {
            user.PersonalData.FirstName = nameAndSurnameDto.FirstName;
            user.PersonalData.LastName = nameAndSurnameDto.LastName;
            user.PersonalData.Newsletter = nameAndSurnameDto.Newsletter;
        }

        internal static void ConvertProduct(ref Product product, ProductDto productDto)
        {
            product.Description = productDto.Description;
            product.ProductPhotos = productDto.ProductPhotos;
            product.RelatedProducts = productDto.RelatedProducts;
            product.Tags = productDto.Tags;
            product.TechnicalData = productDto.TechnicalData;
        }
    }
}
