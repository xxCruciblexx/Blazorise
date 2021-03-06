---
title: "Validation"
permalink: /docs/components/validation/
excerpt: "Learn how to use automatic and manual validation for input components."
toc: true
toc_label: "Guide"
---

Validation component is used to provide simple form validation for Blazorise input components. The basic structure for validation component is:

- `<Validations>` optional container for manual validation
  - `<Validation>` input container
    - `<Feedback>` messages placeholder
      - `<ValidationSuccess>` success message
      - `<ValidationError>` error message
      - `<ValidationNone>` message when nothing has happened
  - `ValidationSummary` lists all error messages

**Notice:** Starting from **v0.9** it is advised to also surround `Field` components with `Validation` tags. This will ensure that validation will work in all scenarios!
{: .notice--warning}

## Basic validation

For the most part you will need to use just the `<Validation>` component along with `<ValidationSuccess>` and `<ValidationError>`. By default every validation will run automatically when input value changes. You must set the `Validator` event handler where you can define the validation rules and return the validation result.}

### Example

Here you can see the basic example for automatic validation and a custom function for checking the email.

```html
<Validation Validator="@ValidateEmail">
    <TextEdit Placeholder="Enter email">
        <Feedback>
            <ValidationNone>Please enter the email.</ValidationNone>
            <ValidationSuccess>Email is good.</ValidationSuccess>
            <ValidationError>Enter valid email!</ValidationError>
        </Feedback>
    </TextEdit>
</Validation>
@code{
    void ValidateEmail( ValidatorEventArgs e )
    {
        var email = Convert.ToString( e.Value );

        e.Status = string.IsNullOrEmpty( email ) ? ValidationStatus.None :
            email.Contains( "@" ) ? ValidationStatus.Success : ValidationStatus.Error;
    }
}
```

The same structure is for all **Edit** components(check, radio, select, etc). Note that for some components there are some special rules when defining the validation structure. For example for **Check** you must use `ChildContent` tag along with the `<Feedback>` tag. This is a limitation in Blazor, hopefully it will be fixed in the future.

```html
<Validation Validator="@ValidateCheck">
    <Check TValue="bool">
        <ChildContent>
            Check me out
        </ChildContent>
        <Feedback>
            <ValidationError>You must check me out!</ValidationError>
        </Feedback>
    </Check>
</Validation>
```

## Manual validation

Sometimes you don't want to do validation on every input change. In that case you use `<Validations>` component to group multiple validations and then run the validation manually.

### Example

In this example you can see how the `<Validations>` component is used to enclose multiple validation components and the `Mode` attribute is set to Manual. Validation is executed only when clicked on submit button.

```html
<Validations @ref="validations" Mode="ValidationMode.Manual">
    <Validation Validator="@ValidateEmail">
        ...
    </Validation>
    <Validation Validator="@ValidatePassword">
        ...
    </Validation>
    <Button Color="Color.Primary" Clicked="@Submit">Submit</Button>
</Validations>
@code{
    Validations validations;

    void Submit()
    {
        validations.ValidateAll();
    }
}
```

## Pattern validation

If you want to validate input by using regular expression instead of `Validator` handlers you can use `Pattern` attribute. Components that supports pattern attribute are `TextEdit`, `NumericEdit` and `DateEdit`.

### Example

```html
<Validation UsePattern="true">
    <TextEdit Pattern="[A-Za-z]{3}">
        <Feedback>
            <ValidationError>Pattern does not match!</ValidationError>
        </Feedback>
    </TextEdit>
</Validation>
```

## Data Annotations

To use data annotations with Blazorise you must combine both `Validation` and the `Validations` components. The `Validations` component will act as a group for a fields used inside of `Validation` component. To make it all work you must meet two requirements:

1. `Validations` component must contain reference to the validated POCO through the `Model` attribute.
2. Input component must bind to the model field through the `@bind-{Value}`(i.e. `@bind-Text`)

After those two requirements are met the Blazorise will have enough information to know how to use data annotations.

### Example

```html
<Validations Mode="ValidationMode.Auto" Model="@user">
    <Validation>
        <Field Horizontal="true">
            <FieldLabel ColumnSize="ColumnSize.Is2">Full Name</FieldLabel>
            <FieldBody ColumnSize="ColumnSize.Is10">
                <TextEdit Placeholder="First and last name" @bind-Text="@user.Name">
                    <Feedback>
                        <ValidationError />
                    </Feedback>
                </TextEdit>
            </FieldBody>
        </Field>
    </Validation>
    <Validation>
        <Field Horizontal="true">
            <FieldLabel ColumnSize="ColumnSize.Is2">Email</FieldLabel>
            <FieldBody ColumnSize="ColumnSize.Is10">
                <TextEdit Placeholder="Enter email" @bind-Text="@user.Email">
                    <Feedback>
                        <ValidationError />
                    </Feedback>
                </TextEdit>
            </FieldBody>
        </Field>
    </Validation>
    <Validation>
        <Field Horizontal="true">
            <FieldLabel ColumnSize="ColumnSize.Is2">Password</FieldLabel>
            <FieldBody ColumnSize="ColumnSize.Is10">
                <TextEdit Role="TextRole.Password" Placeholder="Password" @bind-Text="@user.Password">
                    <Feedback>
                        <ValidationError />
                    </Feedback>
                </TextEdit>
            </FieldBody>
        </Field>
    </Validation>
    <Validation>
        <Field Horizontal="true">
            <FieldLabel ColumnSize="ColumnSize.Is2">Re Password</FieldLabel>
            <FieldBody ColumnSize="ColumnSize.Is10">
                <TextEdit Role="TextRole.Password" Placeholder="Retype password" @bind-Text="@user.ConfirmPassword">
                    <Feedback>
                        <ValidationError />
                    </Feedback>
                </TextEdit>
            </FieldBody>
        </Field>
    </Validation>
</Validations>
```

### Model

```cs
public class User
{
    [Required]
    [StringLength( 10, ErrorMessage = "Name is too long." )]
    public string Name { get; set; }

    [Required]
    [EmailAddress( ErrorMessage = "Invalid email." )]
    public string Email { get; set; }

    [Required( ErrorMessage = "Password is required" )]
    [StringLength( 8, ErrorMessage = "Must be between 5 and 8 characters", MinimumLength = 5 )]
    [DataType( DataType.Password )]
    public string Password { get; set; }

    [Required( ErrorMessage = "Confirm Password is required" )]
    [StringLength( 8, ErrorMessage = "Must be between 5 and 8 characters", MinimumLength = 5 )]
    [DataType( DataType.Password )]
    [Compare( "Password" )]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Title { get; set; }

    [Range( typeof( bool ), "true", "true", ErrorMessage = "You gotta tick the box!" )]
    public bool TermsAndConditions { get; set; }
}
```

**Note:** For a full source code you can look at the [validation page](https://github.com/stsrki/Blazorise/blob/master/Demos/Blazorise.Demo/Pages/Tests/ValidationsPage.razor) inside of a demo application.
{: .notice--info}

## Validation summary

Sometimes you don't want to show error messages under each field. In those situations you can use `ValidationSummary` component. Once placed inside of `Validations` it will show all error messages as a bullet list.

```html
<Validations @ref="annotationsValidations" Mode="ValidationMode.Manual" Model="@manualUser">
    <ValidationSummary Label="Following error occurs..." />

    // other validation fields
</Validations>
```

## Validation rules

In Blazorise you can use some of the predefined validation rules. eg

```html
<Validation Validator="@ValidationRule.IsNotEmpty">
```

Here is a list of the validators currently available.

| Validator                    | Description                                                        |
|------------------------------|--------------------------------------------------------------------|
| IsEmpty                      | Check if the string is null or empty.                              |
| IsNotEmpty                   | Check if the string is not null or empty.                          |
| IsEmail                      | Check if the string is an email.                                   |
| IsAlpha                      | Check if the string contains only letters (a-zA-Z).                |
| IsAlphanumeric               | Check if the string contains only letters and numbers.             |
| IsAlphanumericWithUnderscore | Check if the string contains only letters, numbers and underscore. |
| IsUppercase                  | Check if the string is uppercase.                                  |
| IsLowercase                  | Check if the string is lowercase.                                  |

## Attributes

### Validations

| Name                      | Type                                                                              | Default  | Description                                                                                            |
|---------------------------|-----------------------------------------------------------------------------------|----------|--------------------------------------------------------------------------------------------------------|
| Mode                      | [ValidationMode]({{ "/docs/helpers/enums/#validationmode" | relative_url }})      | `Auto`   | Defines the validation mode for validations inside of this container.                                  |
| Model                     | object                                                                            | null     | Specifies the top-level model object for the form. An edit context will be constructed for this model. |
| MissingFieldsErrorMessage | string                                                                            |          | Message that will be displayed if any of the validations does not have defined error message.          |
| ValidatedAll              | EventCallback                                                                     |          | Event is fired only after all of the validation are successful.                                        |
| StatusChanged             | EventCallback                                                                     |          | Event is fired whenever there is a change in validation status.                                        |

### Validation

| Name         | Type                                                                              | Default  | Description                                                                                |
|--------------|-----------------------------------------------------------------------------------|----------|--------------------------------------------------------------------------------------------|
| Status       | [ValidationStatus]({{ "/docs/helpers/enums/#validationstatus" | relative_url }})  | `None`   | Gets or sets the current validation status.                                                |
| Validator    | action                                                                            |          | Validates the input value after it has being changed.                                      |
| UsePattern   | boolean                                                                           | false    | Forces validation to use regex pattern matching instead of default validator handler.      |
