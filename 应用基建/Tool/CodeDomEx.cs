using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace dotNetLab.Common.Tool
{
  public  class CodeDomEx
    {
       public CodeCompileUnit targetUnit;
       public CodeTypeDeclaration targetClass;
       public string outputFileName = "D:/SampleCode.cs";

        public CodeDomEx()
        {
             
          targetUnit = new CodeCompileUnit();
        }

        public CodeTypeDeclaration AddClass(string strClassName )
        {
            targetClass = new CodeTypeDeclaration("CodeDOMCreatedClass");
            targetClass.IsClass = true;
            targetClass.TypeAttributes =
                TypeAttributes.Public ;
            return targetClass;
        }
        public CodeNamespace AddNamespce(String strNameSpace)
        {
            CodeNamespace samples = new CodeNamespace(strNameSpace);
            targetUnit.Namespaces.Add(samples);
            return targetUnit.Namespaces[targetUnit.Namespaces.Count - 1];
        }
        public void AddImports(CodeNamespace ns, string refName)
        {
            ns.Imports.Add(new CodeNamespaceImport(refName));
        }
        public void AddField(MemberAttributes Modifier,String FieldName
            ,Type FieldType,String FiledComment)
        {
            // Declare the widthValue field.
            CodeMemberField ThisField = new CodeMemberField();
            ThisField.Attributes = Modifier;
            ThisField.Name = FieldName;
            ThisField.Type = new CodeTypeReference(FieldType);
            ThisField.Comments.Add(new CodeCommentStatement(
               FiledComment));
            targetClass.Members.Add(ThisField);
        }

        public void AddProperty(MemberAttributes Modifier,String FieldName,
            String PropertyName,bool HasGet,bool HasSet,
            Type PropertyType,String PropertyComments)
        {
            // Declare the read-only Width property.
            CodeMemberProperty widthProperty = new CodeMemberProperty();
            widthProperty.Attributes = Modifier;
            widthProperty.Name = PropertyName;
            widthProperty.HasGet = HasGet;
            widthProperty.HasSet = HasSet;
            widthProperty.Type = new CodeTypeReference(PropertyType);
            widthProperty.Comments.Add(new CodeCommentStatement(
                PropertyComments));
            if (HasGet)
            {
                widthProperty.GetStatements.Add(new CodeMethodReturnStatement(
                    new CodeFieldReferenceExpression(
                    new CodeThisReferenceExpression(), FieldName)));
            }
            if(HasSet)
            {
                widthProperty.SetStatements.Add(
                   new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), FieldName),
                                    new CodePropertySetValueReferenceExpression()));
            }
            targetClass.Members.Add(widthProperty);
        }

        public void AddMethod(MemberAttributes Modifier,String MethodName,Type ReturnType)
        {
            // Declaring a ToString method
            CodeMemberMethod toStringMethod = new CodeMemberMethod();
            toStringMethod.Attributes = Modifier;
            toStringMethod.Name = MethodName;
            toStringMethod.ReturnType =
                new CodeTypeReference( ReturnType );

            //CodeFieldReferenceExpression widthReference =
            //    new CodeFieldReferenceExpression(
            //    new CodeThisReferenceExpression(), "Width");
            //CodeFieldReferenceExpression heightReference =
            //    new CodeFieldReferenceExpression(
            //    new CodeThisReferenceExpression(), "Height");
            //CodeFieldReferenceExpression areaReference =
            //    new CodeFieldReferenceExpression(
            //    new CodeThisReferenceExpression(), "Area");

            //// Declaring a return statement for method ToString.
            //CodeMethodReturnStatement returnStatement =
            //    new CodeMethodReturnStatement();

            //// This statement returns a string representation of the width,
            //// height, and area.
            //string formattedOutput = "The object:" + Environment.NewLine +
            //    " width = {0}," + Environment.NewLine +
            //    " height = {1}," + Environment.NewLine +
            //    " area = {2}";
            //returnStatement.Expression =
            //    new CodeMethodInvokeExpression(
            //    new CodeTypeReferenceExpression("System.String"), "Format",
            //    new CodePrimitiveExpression(formattedOutput),
            //    widthReference, heightReference, areaReference);
            //toStringMethod.Statements.Add(returnStatement);
            //targetClass.Members.Add(toStringMethod);
              
        }
        public void AddConstructor()
        {
            // Declare the constructor
            CodeConstructor constructor = new CodeConstructor();
            constructor.Attributes =
                MemberAttributes.Public | MemberAttributes.Final;

            // Add parameters.
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(
                typeof(System.Double), "width"));
            constructor.Parameters.Add(new CodeParameterDeclarationExpression(
                typeof(System.Double), "height"));

            // Add field initialization logic
            CodeFieldReferenceExpression widthReference =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "widthValue");
            constructor.Statements.Add(new CodeAssignStatement(widthReference,
                new CodeArgumentReferenceExpression("width")));
            CodeFieldReferenceExpression heightReference =
                new CodeFieldReferenceExpression(
                new CodeThisReferenceExpression(), "heightValue");
            constructor.Statements.Add(new CodeAssignStatement(heightReference,
                new CodeArgumentReferenceExpression("height")));
            targetClass.Members.Add(constructor);
        }

    }
}
