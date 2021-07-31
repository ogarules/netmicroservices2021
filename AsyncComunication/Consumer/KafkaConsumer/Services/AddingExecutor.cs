namespace KafkaConsumer.Services
{
    public class AddingExecutor : MyAbstractBase
    {
        protected override int ExecuteOperation(int val1, int val2)
        {
            return val1 + val2;
        }

        protected override int SetDefaultValue(int val)
        {
            return 0;
        }
    }
}