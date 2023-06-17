using AutoMapper;
using System.Reflection;

namespace webapi.Commons
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMapCustom(typeof(T), GetType())
            .ReverseMapCustom(GetType(), typeof(T));
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}

    public static class MappingProfileExtension
    {

        public static IMappingExpression CreateMapCustom(this Profile profile, Type sourceType, Type destinationType)
        {
            return profile.CreateMap(sourceType, destinationType);
            //var expression = profile.CreateMap(sourceType, destinationType);
            //return MapIgnoreNoneExist(expression, sourceType, destinationType);
        }
        public static IMappingExpression ReverseMapCustom(this IMappingExpression expression, Type sourceType, Type destinationType)
        {
            return expression.ReverseMap();
            //expression = expression.ReverseMap();
            //return MapIgnoreNoneExist(expression, sourceType, destinationType);
        }

    }
