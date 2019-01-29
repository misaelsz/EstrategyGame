
public interface IResouceReceiver {

    ResourceType[] AcceptedResources
    {
        get;
    }

    void ReceiveResource(int amount, ResourceType resource);
    bool AcceptResource(ResourceType resource);

}
