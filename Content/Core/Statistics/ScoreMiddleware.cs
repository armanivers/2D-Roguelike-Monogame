using System;
using System.Collections.Generic;
using System.Text;

namespace _2DRoguelike.Content.Core.Statistics
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class ScoreMiddleware
    {

        private ScoreRank[] rankField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Rank")]
        public ScoreRank[] Rank
        {
            get
            {
                return this.rankField;
            }
            set
            {
                this.rankField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class ScoreRank
    {

        private string nameField;

        private byte scoreField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public byte Score
        {
            get
            {
                return this.scoreField;
            }
            set
            {
                this.scoreField = value;
            }
        }
    }


}


