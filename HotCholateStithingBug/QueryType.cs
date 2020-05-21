using HotChocolate.Types;

namespace HotCholateStithingBug
{
    public class QueryType
        : ObjectType<Query>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(t => t.getids(default))
                .Argument("ids", a => a.Type<ListType<NonNullType<IntType>>>())
                .Type<ListType<NonNullType<IntType>>>();
        }
    }

    public class QueryType2
        : ObjectType<Query>
    {
        protected override void Configure(
            IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field(t => t.getids(default))
                .Argument("ids", a => a.Type<ListType<NonNullType<IntType>>>())
                .Type<ListType<NonNullType<IntType>>>();
        }
    }
}
