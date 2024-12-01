using UnityEngine;

public interface INode
{
    bool RunNode();
}

public class MoveNode : INode
{
    public bool RunNode()
    {
        bool result = false;

        return result;
    }
}


public class BehaviorTree : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
