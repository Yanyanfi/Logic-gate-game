//public class CircuitCompiler
//{
//    private GridManager circuitManager;

//    public CircuitCompiler(GridManager manager)
//    {
//        circuitManager = manager;
//    }

//    public void Compile()
//    {
//        foreach (var component in circuitManager.GetAllComponents())
//        {
//            if (component is LogicGate logicGate)
//            {
//                // 遍历所有线路，找到连接在逻辑门输入引脚上的线路
//                foreach (var wire in circuitManager.GetAllWires())
//                {
//                    // 检查线路的终点是否为该逻辑门的输入引脚
//                    if (IsConnected(wire.endComponent, logicGate))
//                    {
//                        logicGate.inputWires.Add(wire);
//                    }

//                    // 检查线路的起点是否为该逻辑门的输出引脚
//                    if (IsConnected(wire.startComponent, logicGate))
//                    {
//                        logicGate.outputWire = wire;
//                    }
//                }

//                // 让逻辑门订阅其输入线路
//                logicGate.SubscribeToInputs();
//            }
//        }
//    }

//    private bool IsConnected(CircuitComponent pinComponent, LogicGate logicGate)
//    {
//        // 这里可以根据具体坐标或逻辑判断是否连接
//        return pinComponent == logicGate;
//    }
//}
