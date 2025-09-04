using BaseWeb_lab4.Models;
using HotChocolate.Types;

namespace BaseWeb_lab4.GraphQL
{
    public class ApartmentType : ObjectType<Apartment>
    {
        protected override void Configure(IObjectTypeDescriptor<Apartment> descriptor)
        {
            descriptor.Description("Represents an apartment for sale");

            descriptor
                .Field(a => a.Id)
                .Description("The unique identifier of the apartment");

            descriptor
                .Field(a => a.District)
                .Description("The district where the apartment is located");

            descriptor
                .Field(a => a.Floor)
                .Description("The floor number of the apartment");

            descriptor
                .Field(a => a.Area)
                .Description("The area of the apartment in square meters");

            descriptor
                .Field(a => a.Rooms)
                .Description("The number of rooms in the apartment");

            descriptor
                .Field(a => a.Owner)
                .Description("The owner of the apartment");

            descriptor
                .Field(a => a.Price)
                .Description("The price of the apartment");
        }
    }
}