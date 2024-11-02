namespace Social.Net.Core.Attributes.DependencyRegistrars;

[AttributeUsage(AttributeTargets.Class)]
public abstract class DependencyAttribute(Type dependencyType) : Attribute
{
    public readonly Type DependencyType = dependencyType;
}

public class ScopedDependencyAttribute(Type dependencyType) : DependencyAttribute(dependencyType)
{ }