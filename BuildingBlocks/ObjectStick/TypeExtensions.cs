using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bigstick.BuildingBlocks.ObjectStick
{
    public static class TypeExtensions
    {
        public static Type GetTypeMember(MemberInfo info)
        {
            switch (info.MemberType)
            {
               
                case MemberTypes.Field:
                    return ((FieldInfo)info).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)info).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)info).PropertyType;
            }
            return null;
        }
        public static MemberInfo[] GetMembersInclPrivateBase(this Type t, BindingFlags flags)
        {
            var memberList = new List<MemberInfo>();
            memberList.AddRange(t.GetMembers(flags));
            Type currentType = t;
            while ((currentType = currentType.BaseType) != null)
                memberList.AddRange(currentType.GetMembers(flags));
            return memberList.ToArray();
        }

        public static MemberInfo GetMemberInclPrivateBase(this Type t, string memberName)
        {
            var member = t.GetMembersInclPrivateBase(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(x => x.Name.Equals(memberName));
            if (member == null)
                throw new Exception($"Member {memberName} not found.");
            return member;
        }
        
    }
}