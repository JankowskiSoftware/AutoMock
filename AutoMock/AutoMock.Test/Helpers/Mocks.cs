namespace AutoMock.Test.Helpers
{
    public interface IDependency
    {}

    public interface IDependency1
    { }

    public class DependencyImplementation1 : IDependency
    {
    }

    public class DependencyImplementation2 : IDependency
    {
    }

    public class Target_ConstructorsWithDifferentParametersCount
    {
        public Target_ConstructorsWithDifferentParametersCount(IDependency d) { }
        public Target_ConstructorsWithDifferentParametersCount(IDependency d, IDependency1 d1) { }
    }

    public class Target
    {
        public Target() { }
        public Target(IDependency d) { }
    }

    public class Target_ConcreteDependency
    {
        public Target_ConcreteDependency(DependencyImplementation1 d){}
    }

    public class Target_TwoConcreteDependencies
    {
        public Target_TwoConcreteDependencies(DependencyImplementation1 d, DependencyImplementation2 dd) { }
    }

    public class Target_SameTypeDependency
    {
        public Target_SameTypeDependency(IDependency d1, IDependency d2) { }
    }

    public class Target_ValueTypeDependency
    {
        public Target_ValueTypeDependency(
            bool b,
            byte bt,
            char c,
            decimal dc,
            double db,
            float f,
            int i,
            long l,
            sbyte sb,
            short sht,
            uint ui,
            ulong ul,
            ushort ush,
            string s) { }
    }

    public class Target_NoDefaultConstructor
    {
        private Target_NoDefaultConstructor(){}

        public Target_NoDefaultConstructor(IDependency d){}
    }

    public class Target_NoParametrizedConstructor
    {
    }

    public class Target_MultipleConstructorsWithSameParametersCount
    {
        public Target_MultipleConstructorsWithSameParametersCount(IDependency d1, IDependency d2) { }
        public Target_MultipleConstructorsWithSameParametersCount(IDependency1 d1, IDependency1 d2) { }
    }
}