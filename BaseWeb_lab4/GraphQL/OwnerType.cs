using BaseWeb_lab4.Models;
using HotChocolate.Types;

namespace BaseWeb_lab4.GraphQL
{
    public class OwnerType : ObjectType<Owner>
    {
        protected override void Configure(IObjectTypeDescriptor<Owner> descriptor)
        {
            descriptor.Description("Represents an owner of apartments");

            descriptor
                .Field(o => o.Id)
                .Description("The unique identifier of the owner");

            descriptor
                .Field(o => o.Name)
                .Description("The name of the owner");

            descriptor
                .Field(o => o.Phone)
                .Description("The phone number of the owner");

            descriptor
                .Field(o => o.Email)
                .Description("The email address of the owner");

            descriptor
                .Field(o => o.Apartments)
                .Description("The apartments owned by this owner");
        }
    }
}