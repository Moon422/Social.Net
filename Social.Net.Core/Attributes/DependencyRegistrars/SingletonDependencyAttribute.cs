namespace Social.Net.Core.Attributes.DependencyRegistrars;

public class SingletonDependencyAttribute(Type dependencyType) : DependencyAttribute(dependencyType)
{ }