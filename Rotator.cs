using System;

namespace KiParser
{
    public class Rotator
    {
        public Rotator(Bounds bounds, double rotationAngle)
        {
            RotationAngle = rotationAngle;
            Bounds = bounds;
        }

        public Bounds Bounds { get; set; }  

        public double RotationAngle { get; }

        public double RotateX(double x, double y)
        {
            double angle = -Math.PI / 180 * RotationAngle;
            return Math.Cos(angle) * (x - Bounds.XCenter) - Math.Sin(angle) * (y - Bounds.YCenter) + Bounds.XCenter;
        }

        public double RotateY(double x, double y)
        {
            double angle = -Math.PI / 180 * RotationAngle;
            return Math.Sin(angle) * (x - Bounds.XCenter) + Math.Cos(angle) * (y - Bounds.YCenter) + Bounds.YCenter;
        }

        public void RotateParts(Node node)
        {
            node.TraverseAll(RotateNode, SelectNodesWithRotatableParts, this);
        }

        private static bool SelectNodesWithRotatableParts(Node node)
        {
            return !(node is NodeVia);
        }

        private static void RotateNode(Node node, Rotator rotator)
        {
            NodeAt nodeAt = node as NodeAt;

            if (nodeAt != null)
            {
                nodeAt.Rotate(rotator);
            }
        }

        public void RotatePoints(Node node)
        {
            node.TraverseAll(RotatePoint, SelectNodesWithPoints, this);
        }

        private static bool SelectNodesWithPoints(Node node)
        {
            if (node is NodeFpLine || node is NodeFpText || node is NodePad)
            {
                return false;
            }

            return true;
        }

        private static void RotatePoint(Node node, Rotator rotator)
        {
            RotateNode<NodeAt>(rotator, node);
            RotateNode<NodeStart>(rotator, node);
            RotateNode<NodeEnd>(rotator, node);
        }

        private static void RotateNode<T>(Rotator rotator, Node node) where T:Node
        {
            T subNode = node as T;
            if (subNode == null)
            {
                return;
            }

            var newX = rotator.RotateX(node.V1, node.V2);
            var newY = rotator.RotateY(node.V1, node.V2);
            node.V1 = newX;
            node.V2 = newY;
        }
    }
}