using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Core
{
    public class Entity
    {
        private List<Component> _components;
        public int id { get; private set;}
        internal Entity(int _id, List<Component> components)
        {
            id = _id;
            _components = components;
        }
       

        internal Component GetComponent(string componentName)
        {
            Component maybeComponent = _components.Find(c => c.Name.Equals(componentName));
            if (maybeComponent == null)
            {
                throw new ArgumentException("The entity does not contain the component", componentName);
            }
            else
            {
                return maybeComponent;
            }
        }
        //public Component GetComponent1<ComponentType>(String componentName)
        //{
        //    Component maybeComponent = _components.Find(c => c.Name.Equals(componentName));
        //    Assembly assem = typeof(Entity).Assembly;
        //    Type returnType = assem.GetType(componentName);
        //    if (maybeComponent == null)
        //    {
        //        throw new ArgumentException("The entity does not contain the component", componentName);
        //    }
        //    else
        //    {
        //        return (returnType.GetType())maybeComponent;
        //    }
        //}

        public bool HasComponent(string componentName)
        {
            return _components.Exists(c => c.Name.Equals(componentName));
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder("Entity: ");
            sb.AppendFormat("id: {0}", this.id);
            return sb.ToString();

        }

        public override bool Equals(object obj)
        {
            return this.id == ((Entity)obj).id;
        }
        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }



    }

}
