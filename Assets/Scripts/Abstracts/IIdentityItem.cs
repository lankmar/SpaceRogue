namespace Abstracts
{
    public interface IIdentityItem<out TType>
    {
        public TType Id { get; }
    }
}