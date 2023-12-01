## BNS Database
No content yet

## Models
### Table
collection of record

| Field | Describe | 
| :----:  | :----: |
| Name | name |
| Owner | Parent database |
| Definition | data struct definition |
| Records | element collection |

### Record
data element
| Field | Describe | 
| :----:  | :----: |
| Owner | Parent table |
| ElDefinition |  |
| Attributes | [Collection of attribute](#AttributeCollection) |
| Children |  |
| |
| Data         | Raw data |
| XmlNodeType  |  |
| SubclassType |  |
| Ref          | Primary key  |
| StringLookup |  |

### Model Record
Model record is the entity of record, it has the same data as the original record, but it has instance fields.

It was designed for interface presentation, so should not be directly modified.


### AttributeCollection
attributes of the record

This is a dynamic object, so it supports WPF data binding.<br/>
eg. {Binding Record.Attributes.alias}


#### attribute type
+ Int8
+ Int16
+ Int32
+ Int64
+ Float32
+ Bool
+ String
+ Seq
+ Seq16
+ Ref
+ TRef
+ Sub
+ Su
+ Vector16
+ Vector32
+ IColor
+ FColor
+ Box
+ Angle
+ Msec
+ Distance
+ Velocity
+ Prop_seq
+ Prop_field
+ Script_obj
+ Native
+ Version
+ Icon
+ Time32
+ Time64
+ XUnknown1
+ XUnknown2