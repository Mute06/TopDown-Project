using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class DialogueGraph : NodeGraph {

    [HideInInspector]
	public DialogueObject current;

    public void Restart()
    {
        //Find the first DialogueNode without any inputs. This is the starting node.
        current = nodes.Find(x => x is DialogueObject && x.Inputs.All(y => !y.IsConnected)) as DialogueObject;
    }

}