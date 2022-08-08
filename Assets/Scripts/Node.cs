using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


// Node class - x,y and terrain (static)
// Grid class - collection of nodes, size? (static)
// Path class - has a grid, starting / end points (dynamic, each pathfinding request creates this)

public class PathFindingManager
{   
    private Grid mGrid;
    
    private Node[,] mNodes;


    public void Init(int gridSizeX, int gridSizeY) // terrain data...
    {
        mGrid = new Grid(gridSizeX, gridSizeY);
        mNodes = new Node[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; ++x)
        {
            for (int y = 0; y < gridSizeY; ++y)
            {
                mNodes[x,y] = new Node(true, x, y); // set Neighbors? does Node need to know that? prob not
            }
        }

    }

    public void SetNodeData(int gridX, int gridY, bool passable)
    {
        if (gridX > mGrid.mSizeX || gridY > mGrid.mSizeY)
        {
            return;
        }
        // TODO bounds checking
        mNodes[gridX, gridY].mWalkable = passable;
    }

    public void FindPath(Vector2Int start, Vector2Int end)
    {
        Path path = new Path(mGrid, mNodes[start.x, start.y], mNodes[end.x, end.y], mNodes);
        List<Node> foundPath = path.FindPath();
        Debug.Log("DONE CALCULATING PATH! Result:");
        foreach (Node n in foundPath)
        {
            n.Print();
        }
    }

    public List<Vector3> FindPath2(Vector2Int start, Vector2Int end)
    {
        Path path = new Path(mGrid, mNodes[start.x, start.y], mNodes[end.x, end.y], mNodes);
        List<Node> foundPath = path.FindPath();
        Debug.Log("DONE CALCULATING PATH! Result:");
        foreach (Node n in foundPath)
        {
            n.Print();
        }

        // Found nodes, need to convert back to real world coords
        List<Vector3> result = new List<Vector3>();
        foreach (Node n in foundPath)
        {
            //x starts at 16...
            
            result.Add(new Vector3(n.mGridY * 4.75f, 2, 15 * 4.5f - n.mGridX * 4.5f));
        }
        return result;
    }

}

public class Grid
{
    public int mSizeX;
    public int mSizeY;

    public Grid (int sizeX, int sizeY)
    {
        mSizeX = sizeX;
        mSizeY = sizeY;
    }
}

public class Path
{
    public Node mStartingNode;
    public Node mEndingNode;
    private List<Node> mCalculatedPath = new List<Node>();

    //pointer?
    public Grid mGrid;
    private Node[,] mNodes;
    private Node mCurrentNode;

    public Path(Grid grid, Node startingNode, Node endingNode, Node[,] nodes)
    {
        mGrid = grid;
        mStartingNode = startingNode;
        mEndingNode = endingNode;
        mCurrentNode = mStartingNode;
        mNodes = nodes;
    }

    public List<Node> FindPath()
    {
        int counter = 0;
        int max = 1000;
        while(StepToNextNode() && counter < max)
        {
            ++counter;
            //print?
        }

        // What about returning if it was successful or not? Path is an out param?
        return mCalculatedPath;
    }

    private bool StepToNextNode()
    {
        // We're at the destination already!
        if (mCurrentNode == mEndingNode)
        {
            return false;
        }

        // Calc the cost of all neighbor nodes
        Node nextNode = CalculateNeighbors(mCurrentNode, mStartingNode, mEndingNode);
        if (mCurrentNode != nextNode)
        {
            Debug.Log("Found next node!");
            nextNode.Print();
            mCurrentNode = nextNode;
            mCalculatedPath.Add(mCurrentNode);
            return true;
        }
        // Step
        
        return false;

        // returns false if at target or didn't step
    }

    // Returns the next best node
    private Node CalculateNeighbors(Node currentNode, Node startingNode, Node endingNode)
    {
        // well if current Node is x,y, we need to check each of the 4 directions
        Node nextNode = currentNode;
        int nextCost = Int32.MaxValue;

        // super jank, refactor
        if (currentNode.mGridX < 15)
        {
            int east = CalculateNode(mNodes[currentNode.mGridX + 1, currentNode.mGridY], startingNode, endingNode);
            if (east < nextCost)
            {
                nextNode = mNodes[currentNode.mGridX + 1, currentNode.mGridY];
                nextCost = east;
            }
        }

        if (currentNode.mGridY < 15)
        {
            int north = CalculateNode(mNodes[currentNode.mGridX, currentNode.mGridY + 1], startingNode, endingNode);
            if (north < nextCost)
            {
                nextNode = mNodes[currentNode.mGridX, currentNode.mGridY + 1];
                nextCost = north;
            }
        }

        if (currentNode.mGridX > 0)
        {
            int west = CalculateNode(mNodes[currentNode.mGridX - 1, currentNode.mGridY], startingNode, endingNode);
            if (west < nextCost)
            {
                nextNode = mNodes[currentNode.mGridX - 1, currentNode.mGridY];
                nextCost = west;
            }
        }

        if (currentNode.mGridY > 0)
        {
            int south = CalculateNode(mNodes[currentNode.mGridX, currentNode.mGridY - 1], startingNode, endingNode);
            if (south < nextCost)
            {
                nextNode = mNodes[currentNode.mGridX, currentNode.mGridY - 1];
                nextCost = south;
            }
        }

        return nextNode;

        //throw new NotImplementedException();
        //return null;
    }

    private int CalculateNode(Node node, Node startingNode, Node endingNode)
    {
        // Don't want to retrace our steps or go to a node that isn't passable...
        if (mCalculatedPath.Contains(node) || !node.mWalkable)
        {
            return Int32.MaxValue;
        }

        // TODO - calculate passable terrain flag

        // well if current Node is x,y, we need to check each of the 4 directions

        
        // Dealing with distance, so just going to use absolute values

        int deltaX = endingNode.mGridX - node.mGridX;
        int deltaY = endingNode.mGridY - node.mGridY;
        node.mHCost = Math.Abs(deltaX) + Math.Abs(deltaY);

        // recalc G?

        deltaX = startingNode.mGridX - node.mGridX;
        deltaY = startingNode.mGridY - node.mGridY;
        node.mGCost = Math.Abs(deltaX) + Math.Abs(deltaY);

        return node.mHCost + node.mGCost;
        //return node.M

        //return null;

        // WAIT - I'm not doing diagonal....
        /*
        // well if current Node is x,y, we need to check each of the 4 directions

        
        // Dealing with distance, so just going to use absolute values

        int deltaX = endingNode.mGridX - node.mGridX;
        int deltaY = endingNode.mGridY - node.mGridY;

        int absX = Math.Abs(deltaX);
        int absY = Math.Abs(deltaY);

        //int 
        //int deltaX = Math.Abs(targetNode.mGridX);
        

        

        //h = lowest side * 14, then difference * 10  
       // int lowerDimension = targetNode.mGridX > node.mGridX

        node.mHCost = absX > absY ? 
            ((absY * 14) + ((absX - absY) * 10)) :
            ((absX * 14) + ((absY - absX) * 10));

        // recalc G?

        deltaX = startingNode.mGridX - node.mGridX;
        deltaY = startingNode.mGridY - node.mGridY;

        absX = Math.Abs(deltaX);
        absY = Math.Abs(deltaY);

        node.mGCost = absX > absY ? 
            ((absY * 14) + ((absX - absY) * 10)) :
            ((absX * 14) + ((absY - absX) * 10));


        return node.mHCost + node.mGCost;
        //return node.M

        //return null;
        */
        
    }

    public void PrintCurrentPath()
    {
        throw new NotImplementedException();
    }
}

public class Node
{
    // gCost = cost to get to this node from the starting node
    // hCost = estimated cost to get from this node to the ending node
    // fCost = overall cost (gCost + hCost). Nodes with the lowest fCost values are the ones that get traversed.

    public bool mWalkable;
	//public Vector2 worldPosition;
	public int mGridX;
	public int mGridY;

	public int mGCost;
	public int mHCost;
	//public Node parent;
	//int heapIndex;

    public Node (bool walkable, int gridX, int gridY)
    {
        mWalkable = walkable;
        mGridX = gridX;
        mGridY = gridY;
    }

    public override bool Equals(object obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Node n = (Node) obj;
            return (mGridX == n.mGridX) && (mGridY == n.mGridY);
        }
    }

    public void Print()
    {
        Debug.Log($"({mGridX},{mGridY}) G:{mGCost} H:{mHCost}");
    }
}