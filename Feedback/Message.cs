using System;
namespace judge_c_sharp.Feedback
{
    public class Message
    {
        private Format Format;
        private string Description;
        private Permission Permission;

        public Message(Format format, string content, Permission permission)
        {
            this.Format = format;
            this.Description = content;
            this.Permission = permission;
        }

        public static Message Plain(string content) => new Message(Format.PLAIN, content, Permission.STUDENT);

        public static Message Code(string content) => new Message(Format.CODE, content, Permission.STUDENT);

        public static Message Staff(string content) => new Message(Format.PLAIN, content, Permission.STAFF);

        public static Message InternalError(string content) => new Message(Format.CODE, content, Permission.STAFF);

    }
}
