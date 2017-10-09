/* Options:
Date: 2017-10-10 00:13:23
Version: 1,044
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:5000

//GlobalNamespace: 
//MakePropertiesOptional: True
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//AddDescriptionAsComments: True
//IncludeTypes: 
//ExcludeTypes: 
//DefaultImports: 
*/


export interface IReturnVoid
{
    createResponse() : void;
}

export interface IReturn<T>
{
    createResponse() : T;
}

export class GetZippedLinkResponse
{
    hash: string;
    originalLink: string;
    created: string;
    followed: number;
    zippedLink: string;
}

export type CreatedStatusEnum = "Fail" | "Success";

export class GetAllZippedLinkResponse
{
    links: GetZippedLinkResponse[];
}

export class CreateZippedLinkResponse
{
    status: CreatedStatusEnum;
    statusText: string;
    zippedLink: string;
}

// @Route("/zippedLink/all")
export class GetAllZippedLinkRequest implements IReturn<GetAllZippedLinkResponse>
{
    createResponse() { return new GetAllZippedLinkResponse(); }
    getTypeName() { return "GetAllZippedLinkRequest"; }
}

// @Route("/zippedLink/create")
export class CreateZippedLinkRequest implements IReturn<CreateZippedLinkResponse>
{
    link: string;
    createResponse() { return new CreateZippedLinkResponse(); }
    getTypeName() { return "CreateZippedLinkRequest"; }
}
