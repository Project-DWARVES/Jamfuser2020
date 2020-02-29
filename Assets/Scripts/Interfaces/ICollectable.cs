public interface ICollectable
{
    bool canBePickedUp {get; set;}
    void OnPickup();
    void OnDrop();
}
