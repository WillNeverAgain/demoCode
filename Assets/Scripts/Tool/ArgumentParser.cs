using System;
using System.Collections.Generic;
namespace Tool
{
    public class ArgumentParser
    {
        private IList<(Type,string)> arguments;

        public ArgumentParser()
        {
            arguments = new List<(Type,string)>();
        }
        public void SettingArguments<T>(string argumentDiscrib="value")
        {
            arguments.Add((typeof(T),argumentDiscrib));
        }
        private int index = 0;
        public void Reset()
        {
            index = 0;
        }
        /// <summary>
        /// 参数检验
        /// </summary>
        public T ParserArguments<T>(object value)
        {
            if (arguments.Count <= index)
            {
                throw new IndexOutOfRangeException();
            }
            var temp=arguments[index];
            if ( typeof(T).IsAssignableFrom(temp.Item1))
            {
                index++;
                return (T)value;
            }
            throw new Exception($"{temp.Item2} : {typeof(T).FullName} is not a valid argument {temp.Item1.FullName}  ");
        }
    }
}
