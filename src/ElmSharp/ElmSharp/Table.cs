/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;

namespace ElmSharp
{
    /// <summary>
    /// The Table is a container widget to arrange other widgets in a table where items can span multiple columns or rows .
    /// Inherits <see cref="Container"/>.
    /// </summary>
    public class Table : Container
    {
        int _paddingX = 0;
        int _paddingY = 0;

        /// <summary>
        /// Creates and initializes a new instance of the Table class.
        /// </summary>
        /// <param name="parent">
        /// A <see cref="EvasObject"/> to which the new Table instance will be attached.
        /// </param>
        public Table(EvasObject parent) : base(parent)
        {
        }

        /// <summary>
        /// Sets or gets whether the layout of this table is homogeneous.
        /// </summary>
        /// <remarks>True for homogeneous, False for no homogeneous</remarks>
        public bool Homogeneous
        {
            get
            {
                return Interop.Elementary.elm_table_homogeneous_get(RealHandle);
            }
            set
            {
                Interop.Elementary.elm_table_homogeneous_set(RealHandle, value);
            }
        }

        /// <summary>
        /// Sets or gets the horizontal padding between the cells.
        /// </summary>
        public int PaddingX
        {
            get
            {
                return _paddingX;
            }
            set
            {
                _paddingX = value;
                Interop.Elementary.elm_table_padding_set(RealHandle, _paddingX, _paddingY);
            }
        }

        /// <summary>
        /// Sets or gets the vertical padding between the cells.
        /// </summary>
        public int PaddingY
        {
            get
            {
                return _paddingY;
            }
            set
            {
                _paddingY = value;
                Interop.Elementary.elm_table_padding_set(RealHandle, _paddingX, _paddingY);
            }
        }
        /// <summary>
        /// Adds a subobject on the table with the coordinates passed.
        /// </summary>
        /// <param name="obj">The subobject to be added to the table</param>
        /// <param name="col">The column number</param>
        /// <param name="row">The row number</param>
        /// <param name="colspan">The column span</param>
        /// <param name="rowspan">The row span</param>
        public void Pack(EvasObject obj, int col, int row, int colspan, int rowspan)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            Interop.Elementary.elm_table_pack(RealHandle, obj, col, row, colspan, rowspan);
            AddChild(obj);
        }

        /// <summary>
        /// Removes the child from the table.
        /// </summary>
        /// <param name="obj">The subobject</param>
        public void Unpack(EvasObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");
            Interop.Elementary.elm_table_unpack(RealHandle, obj);
            RemoveChild(obj);
        }

        /// <summary>
        /// Removes all child objects from a table object.
        /// </summary>
        public void Clear()
        {
            Interop.Elementary.elm_table_clear(RealHandle, false);
            ClearChildren();
        }

        /// <summary>
        /// Sets the color for particular part of the table.
        /// </summary>
        /// <param name="part">The name of part class</param>
        /// <param name="color">The color</param>
        public override void SetPartColor(string part, Color color)
        {
            Interop.Elementary.elm_object_color_class_color_set(Handle, part, color.R * color.A / 255,
                                                                              color.G * color.A / 255,
                                                                              color.B * color.A / 255,
                                                                              color.A);
        }

        /// <summary>
        /// Gets the color of particular part of the table.
        /// </summary>
        /// <param name="part">The name of part class, it could be 'bg', 'elm.swllow.content'</param>
        /// <returns>The color of the particular part</returns>
        public override Color GetPartColor(string part)
        {
            int r, g, b, a;
            Interop.Elementary.elm_object_color_class_color_get(Handle, part, out r, out g, out b, out a);
            return new Color((int)(r / (a / 255.0)), (int)(g / (a / 255.0)), (int)(b / (a / 255.0)), a);
        }

        protected override IntPtr CreateHandle(EvasObject parent)
        {
            IntPtr handle = Interop.Elementary.elm_layout_add(parent);
            Interop.Elementary.elm_layout_theme_set(handle, "layout", "background", "default");

            RealHandle = Interop.Elementary.elm_table_add(handle);
            Interop.Elementary.elm_object_part_content_set(handle, "elm.swallow.content", RealHandle);

            return handle;
        }
    }
}
