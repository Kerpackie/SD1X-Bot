using Discord;
using System;

namespace Bot.Common
{
    class SP1XEmbedBuilder
    {
        private string title;
        private string description;
        private string footer;
        private string thumbnail;
        private string image;
        private EmbedStyle style;

        /// <summary>
        /// Gets or sets the title of the embed.
        /// </summary>
        public string Title
        {
            get => this.Title;
            set
            {
                if (value?.Length > 256)
                {
                    throw new ArgumentException(message: $"Title length must be less than or equal to 256.",
                        paramName: nameof(this.Title));
                }

                this.title = value;
            }
        }

        /// <summary>
        /// Gets or sets the description of the embed.
        /// </summary>
        public string Description
        {
            get => this.description;
            set
            {
                if (value?.Length > 2048)
                {
                    throw new ArgumentException(message: $"Description length must be less than or equal to 2048.",
                        paramName: nameof(this.Description));
                }

                this.description = value;
            }
        }

        /// <summary>
        /// Gets or sets the footer of the embed.
        /// </summary>
        public string Footer
        {
            get => this.footer;
            set
            {
                if (value?.Length > 2048)
                {
                    throw new ArgumentException(message: $"Footer length must be less than or equal to 2048.",
                        paramName: nameof(this.Footer));
                }

                this.footer = value;
            }
        }

        /// <summary>
        /// Gets or sets and image if it is included in the embed.
        /// </summary>
        public string Image
        {
            get => this.Image;
            set
            {
                if (value?.Length > 256)
                {
                    throw new ArgumentException(message: $"Image URL must be less than or equal to 256.",
                        paramName: nameof(this.Title));
                }

                this.image = value;
            }
        }

        /// <summary>
        /// Gets or sets and thumbnail if it is included in the embed.
        /// </summary>
        public string Thumbnail
        {
            get => this.Thumbnail;
            set
            {
                if (value?.Length > 256)
                {
                    throw new ArgumentException(message: $"Image URL must be less than or equal to 256.",
                        paramName: nameof(this.Title));
                }

                this.thumbnail = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="EmbedStyle"/> of the embed.
        /// </summary>
        public EmbedStyle Style
        {
            get => this.style;
            set { this.style = value; }
        }

        /// <summary>
        /// Attach the title to the embed.
        /// </summary>
        /// <param name="title">The title of the embed.</param>
        /// <returns>A <see cref="SP1XEmbedBuilder"/> with an attached title.</returns>
        public SP1XEmbedBuilder WithTitle(string title)
        {
            this.Title = title;
            return this;
        }

        /// <summary>
        /// Attach the description to the embed.
        /// </summary>
        /// <param name="description">The description of the embed.</param>
        /// <returns>A <see cref="SP1XEmbedBuilder"/> with an attached description.</returns>
        public SP1XEmbedBuilder WithDescription(string description)
        {
            this.Description = description;
            return this;
        }

        /// <summary>
        /// Attach a footer to the embed.
        /// </summary>
        /// <param name="footer">The footer of the embed.</param>
        /// <returns>A <see cref="SP1XEmbedBuilder"/> with an attached footer.</returns>
        public SP1XEmbedBuilder WithFooter(string footer)
        {
            this.Footer = footer;
            return this;
        }

        /// <summary>
        /// Attach an image to the embed.
        /// </summary>
        /// <param name="image">The image attached to the embed.</param>
        /// <returns>A <see cref="SP1XEmbedBuilder"/> with an attached image.</returns>
        public SP1XEmbedBuilder WithImage(string image)
        {
            this.Image = image;
            return this;
        }

        /// <summary>
        /// Attach an thumbnail to the embed.
        /// </summary>
        /// <param name="thumbnail">The thumbnail attached to the embed.</param>
        /// <returns>A <see cref="SP1XEmbedBuilder"/> with an attached thumbnail.</returns>
        public SP1XEmbedBuilder WithThumbnail(string thumbnail)
        {
            this.Thumbnail = thumbnail;
            return this;
        }

        /// <summary>
        /// Attach an <see cref="EmbedStyle"/> to the embed.
        /// </summary>
        /// <param name="style">The <see cref="EmbedStyle"/> of the embed.</param>
        /// <returns>A <see cref="SP1XEmbedBuilder"/> with an attached <see cref="EmbedStyle"/>.</returns>
        public SP1XEmbedBuilder WithStyle(EmbedStyle style)
        {
            this.Style = style;
            return this;
        }

        /// <summary>
        /// Builds the <see cref="SP1XEmbedBuilder"/>.
        /// </summary>
        /// <returns>The <see cref="Embed"/> with all attached properties.</returns>
        public Embed Build()
        {
            EmbedBuilder builder = new EmbedBuilder()
                .WithDescription(this.description)
                .WithThumbnailUrl(this.thumbnail)
                .WithImageUrl(this.image)
                .WithFooter(this.footer);

            switch (this.style)
            {
                case EmbedStyle.Success:
                    builder
                        .WithColor(Colors.Success)
                        .WithAuthor(x =>
                        {
                            x
                                .WithIconUrl(Icons.Success)
                                .WithName(this.title);
                        });
                    break;

                case EmbedStyle.Error:
                    builder
                        .WithColor(Colors.Error)
                        .WithAuthor(x =>
                        {
                            x
                                .WithIconUrl(Icons.Error)
                                .WithName(this.title);
                        });
                    break;

                case EmbedStyle.Information:
                    builder
                        .WithColor(Colors.Information)
                        .WithAuthor(x =>
                        {
                            x
                                .WithIconUrl(Icons.Information)
                                .WithName(this.title);
                        });
                    break;

                case EmbedStyle.Image:
                    builder
                        .WithTitle(this.title)
                        .WithColor(Colors.Quest);
                    break;
            }

            return builder.Build();
        }
    }
}
