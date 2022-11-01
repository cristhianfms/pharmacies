export interface Invitation {
    invitationCode: string,
    userName: string,
    roleName: string,
    pharmacyName: string,
    used: boolean
}

export interface CreateInvitationDto extends Omit<Invitation, 'id'> {

}

export interface UpdateInvitationDto {
    userName: string;
    email: string;
    address: string;
    password:string;
    roleName: string;
    pharmacyName: string;
}
