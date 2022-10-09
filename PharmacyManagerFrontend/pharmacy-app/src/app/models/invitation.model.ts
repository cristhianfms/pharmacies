export interface Invitation {
    id: number
    userName: string,
    roleName: string,
    pharmacyName: string
}

export interface CreateInvitationDto extends Omit<Invitation, 'id'> {

}

export interface UpdateInvitationDto extends Partial<CreateInvitationDto> {

}
