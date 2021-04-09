﻿/*
 * Program : Clash Of SL Server
 * Description : A C# Writted 'Clash of SL' Server Emulator !
 *
 * Authors:  Sky Tharusha <Founder at Sky Production>,
 *           And the Official DARK Developement Team
 *
 * Copyright (c) 2021  Sky Production
 * All Rights Reserved.
 */

using System;
using System.IO;
using System.Text;
using CSS.Core;

namespace CSS.PacketProcessing
{
    /// <summary>
    ///     Wrapper of <see cref="BinaryReader" /> that implements methods to read Clash of Clans mesasges.
    /// </summary>
    public class MessageReader : BinaryReader
    {
        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageReader" /> class based on the
        ///     specified stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        public MessageReader(Stream input) : base(input)
        {
            // Space
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///     Reads a length-prefixed byte array from the current stream and advances the stream
        ///     position by the length of the byte array and the length of the prefix which is 4 bytes long.
        /// </summary>
        /// <returns>A byte array read from the current stream.</returns>
        /// <exception cref="InvalidMessageException">Byte array length is invalid.</exception>
        public byte[] ReadBytes()
        {
            var length = ReadInt32();
            CheckLength(length, "byte array");

            if (length == -1)
                return null;
            return ReadBytes(length);
        }

        /// <summary>
        ///     Reads an 8-byte floating point value from the current stream and advances the current
        ///     position of the stream by eight bytes.
        /// </summary>
        /// <returns>A</returns>
        public override double ReadDouble()
        {
            var buffer = ReadByteArrayEndian(8);
            return BitConverter.ToDouble(buffer, 0);
        }

        /// <summary>
        ///     Reads a 2-byte signed integer from the current stream and advances the current position
        ///     of the stream by two bytes.
        /// </summary>
        /// <returns>A 2-byte signed integer read from the current stream.</returns>
        public override short ReadInt16()
        {
            return (short) ReadUInt16();
        }

        /// <summary>
        ///     Reads a 4-byte signed integer from the current stream and advances the current position
        ///     of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte signed integer read from the current stream.</returns>
        public override int ReadInt32()
        {
            return (int) ReadUInt32();
        }

        /// <summary>
        ///     Reads a 8-byte signed integer from the current stream and advances the current position
        ///     of the stream by four bytes.
        /// </summary>
        /// <returns>A 8-byte signed integer from the current stream.</returns>
        public override long ReadInt64()
        {
            return (long) ReadUInt64();
        }

        /// <summary>
        ///     Reads a 4-byte floating-point value from the current stream and advances the current
        ///     position of the stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte floating-point value from the current stream.</returns>
        public override float ReadSingle()
        {
            var buffer = ReadByteArrayEndian(4);
            return BitConverter.ToSingle(buffer, 0);
        }

        /// <summary>
        ///     Reads a length-prefixed string encoded in UTF-8 from the current stream and advances the
        ///     stream position by the length of the string and the length of the prefix which is 4 bytes long.
        /// </summary>
        /// <returns>A string read from the current stream.</returns>
        /// <exception cref="InvalidMessageException">String length is invalid.</exception>
        public override string ReadString()
        {
            var length = ReadInt32();
            CheckLength(length, "string");

            if (length == -1)
                return null;
            var buffer = ReadBytes(length);
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        ///     Reads a 2-byte unsigned integer from the current stream and advances the position of the
        ///     stream by two bytes.
        /// </summary>
        /// <returns>A 2-byte unsigned integer from the current stream.</returns>
        public override ushort ReadUInt16()
        {
            var buffer = ReadByteArrayEndian(2);
            return BitConverter.ToUInt16(buffer, 0);
        }

        /// <summary>
        ///     Reads a 4-byte unsigned integer from the current stream and advances the position of the
        ///     stream by four bytes.
        /// </summary>
        /// <returns>A 4-byte unsigned integer from the current stream.</returns>
        public override uint ReadUInt32()
        {
            var buffer = ReadByteArrayEndian(4);
            return BitConverter.ToUInt32(buffer, 0);
        }

        /// <summary>
        ///     Reads an 8-byte unsigned integer from the current stream and advances the position of the
        ///     stream by eight bytes.
        /// </summary>
        /// <returns>An 8-byte unsigned integer from the current stream.</returns>
        public override ulong ReadUInt64()
        {
            var buffer = ReadByteArrayEndian(8);
            return BitConverter.ToUInt64(buffer, 0);
        }

        #endregion Public Methods

        #region Private Methods

        void CheckLength(int length, string typeName)
        {
            if (length < -1)
                _Logger.Print("     The length of a " + typeName + " was invalid: " + length,Types.ERROR);

            if (length > BaseStream.Length - BaseStream.Position)
                _Logger.Print("     The length of a " + typeName + " was larger than the remaining bytes: " + length,Types.ERROR);
        }

        byte[] ReadByteArrayEndian(int count)
        {
            var buffer = ReadBytes(count);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            return buffer;
        }

        #endregion Private Methods
    }
}