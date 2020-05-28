// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: FireActionDef.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Mumble.FirstGame.Serialization.Protobuf.Action {

  /// <summary>Holder for reflection information generated from FireActionDef.proto</summary>
  public static partial class FireActionDefReflection {

    #region Descriptor
    /// <summary>File descriptor for FireActionDef.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static FireActionDefReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNGaXJlQWN0aW9uRGVmLnByb3RvEgZhY3Rpb24aD0RpcmVjdGlvbi5wcm90",
            "byJBCg1GaXJlQWN0aW9uRGVmEgoKAmlkGAEgASgFEiQKCWRpcmVjdGlvbhgC",
            "IAEoCzIRLmFjdGlvbi5EaXJlY3Rpb25CMaoCLk11bWJsZS5GaXJzdEdhbWUu",
            "U2VyaWFsaXphdGlvbi5Qcm90b2J1Zi5BY3Rpb25iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::Mumble.FirstGame.Serialization.Protobuf.Action.DirectionReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Mumble.FirstGame.Serialization.Protobuf.Action.FireActionDef), global::Mumble.FirstGame.Serialization.Protobuf.Action.FireActionDef.Parser, new[]{ "Id", "Direction" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class FireActionDef : pb::IMessage<FireActionDef> {
    private static readonly pb::MessageParser<FireActionDef> _parser = new pb::MessageParser<FireActionDef>(() => new FireActionDef());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<FireActionDef> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Mumble.FirstGame.Serialization.Protobuf.Action.FireActionDefReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FireActionDef() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FireActionDef(FireActionDef other) : this() {
      id_ = other.id_;
      direction_ = other.direction_ != null ? other.direction_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public FireActionDef Clone() {
      return new FireActionDef(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "direction" field.</summary>
    public const int DirectionFieldNumber = 2;
    private global::Mumble.FirstGame.Serialization.Protobuf.Action.Direction direction_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Mumble.FirstGame.Serialization.Protobuf.Action.Direction Direction {
      get { return direction_; }
      set {
        direction_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as FireActionDef);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(FireActionDef other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (!object.Equals(Direction, other.Direction)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (direction_ != null) hash ^= Direction.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (direction_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Direction);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (direction_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Direction);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(FireActionDef other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.direction_ != null) {
        if (direction_ == null) {
          Direction = new global::Mumble.FirstGame.Serialization.Protobuf.Action.Direction();
        }
        Direction.MergeFrom(other.Direction);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 18: {
            if (direction_ == null) {
              Direction = new global::Mumble.FirstGame.Serialization.Protobuf.Action.Direction();
            }
            input.ReadMessage(Direction);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code