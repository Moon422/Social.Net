namespace Social.Net.Core.Attributes.DependencyRegistrars;

public class TransientDependencyAttribute(Type dependencyType) : DependencyAttribute(dependencyType)
{ }