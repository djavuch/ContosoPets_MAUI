using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContosoPets.Models;

namespace ContosoPets.Utilities;

public static class PetOwnerDemoDataInitializer
{
    public static IEnumerable<PetOwnerModel> GetDemoPetsOwners()
    {
        return new ObservableCollection<PetOwnerModel>
        {
            new()
            {
                OwnerId = "1",
                OwnerName = "John Doe",
                OwnerPhone = "123-456-7890",
                OwnerEmail = "johndoe@mail.com",
                OwnerAddress = "123 Main St",
                OwnerCity = "Seattle",
                OwnerState = "WA",
                OwnerZipCode = "98101",
                Pets = []
            },
            new()
            {
                OwnerId = "2",
                OwnerName = "Jane Smith",
                OwnerPhone = "987-654-3210",
                OwnerEmail = "j_smith@mail.com",
                OwnerAddress = "456 Elm St",
                OwnerCity = "Bellevue",
                OwnerState = "WA",
                OwnerZipCode = "98004",
                Pets = []
            },
            new()
            {
                OwnerId = "3",
                OwnerName = "Michael Johnson",
                OwnerPhone = "555-123-4567",
                OwnerEmail = "mjohnson@mail.com",
                OwnerAddress = "789 Oak Ave",
                OwnerCity = "Redmond",
                OwnerState = "WA",
                OwnerZipCode = "98052",
                Pets = []
            },
            new()
            {
                OwnerId = "4",
                OwnerName = "Emily Davis",
                OwnerPhone = "555-987-6543",
                OwnerEmail = "emilyd@mail.com",
                OwnerAddress = "321 Pine St",
                OwnerCity = "Kirkland",
                OwnerState = "WA",
                OwnerZipCode = "98033",
                Pets = []
            },
            new()
            {
                OwnerId = "5",
                OwnerName = "William Brown",
                OwnerPhone = "555-222-3333",
                OwnerEmail = "wbrown@mail.com",
                OwnerAddress = "654 Maple Dr",
                OwnerCity = "Issaquah",
                OwnerState = "WA",
                OwnerZipCode = "98027",
                Pets = []
            },
            new()
            {
                OwnerId = "6",
                OwnerName = "Olivia Wilson",
                OwnerPhone = "555-444-5555",
                OwnerEmail = "oliviaw@mail.com",
                OwnerAddress = "987 Cedar Ln",
                OwnerCity = "Sammamish",
                OwnerState = "WA",
                OwnerZipCode = "98074",
                Pets = []
            },
            new()
            {
                OwnerId = "7",
                OwnerName = "James Martinez",
                OwnerPhone = "555-666-7777",
                OwnerEmail = "jmartinez@mail.com",
                OwnerAddress = "159 Spruce Ct",
                OwnerCity = "Renton",
                OwnerState = "WA",
                OwnerZipCode = "98057",
                Pets = []
            },
            new()
            {
                OwnerId = "8",
                OwnerName = "Sophia Anderson",
                OwnerPhone = "555-888-9999",
                OwnerEmail = "sophiaa@mail.com",
                OwnerAddress = "753 Birch Blvd",
                OwnerCity = "Bothell",
                OwnerState = "WA",
                OwnerZipCode = "98011",
                Pets = []
            },
            new()
            {
                OwnerId = "9",
                OwnerName = "Benjamin Lee",
                OwnerPhone = "555-101-2020",
                OwnerEmail = "benlee@mail.com",
                OwnerAddress = "852 Willow Way",
                OwnerCity = "Woodinville",
                OwnerState = "WA",
                OwnerZipCode = "98072",
                Pets = []
            },
            new()
            {
                OwnerId = "10",
                OwnerName = "Ava Thomas",
                OwnerPhone = "555-303-4040",
                OwnerEmail = "avathomas@mail.com",
                OwnerAddress = "951 Aspen Rd",
                OwnerCity = "Lynnwood",
                OwnerState = "WA",
                OwnerZipCode = "98036",
                Pets = []
            },
            new()
            {
                OwnerId = "11",
                OwnerName = "Lucas Garcia",
                OwnerPhone = "555-505-6060",
                OwnerEmail = "lucasg@mail.com",
                OwnerAddress = "357 Poplar St",
                OwnerCity = "Everett",
                OwnerState = "WA",
                OwnerZipCode = "98201",
                Pets = []
            },
            new()
            {
                OwnerId = "12",
                OwnerName = "Mia Rodriguez",
                OwnerPhone = "555-707-8080",
                OwnerEmail = "miarod@mail.com",
                OwnerAddress = "246 Chestnut Ave",
                OwnerCity = "Shoreline",
                OwnerState = "WA",
                OwnerZipCode = "98133",
                Pets = []
            },
            new()
            {
                OwnerId = "13",
                OwnerName = "Henry Clark",
                OwnerPhone = "555-909-1010",
                OwnerEmail = "henryc@mail.com",
                OwnerAddress = "135 Redwood Dr",
                OwnerCity = "Kent",
                OwnerState = "WA",
                OwnerZipCode = "98030",
                Pets = []
            },
            new()
            {
                OwnerId = "14",
                OwnerName = "Charlotte Lewis",
                OwnerPhone = "555-111-2222",
                OwnerEmail = "charlottel@mail.com",
                OwnerAddress = "864 Magnolia Pl",
                OwnerCity = "Auburn",
                OwnerState = "WA",
                OwnerZipCode = "98002",
                Pets = []
            },
            new()
            {
                OwnerId = "15",
                OwnerName = "Jack Walker",
                OwnerPhone = "555-333-4444",
                OwnerEmail = "jackw@mail.com",
                OwnerAddress = "753 Cypress Ln",
                OwnerCity = "Tacoma",
                OwnerState = "WA",
                OwnerZipCode = "98402",
                Pets = []
            },
            new()
            {
                OwnerId = "16",
                OwnerName = "Amelia Hall",
                OwnerPhone = "555-555-6666",
                OwnerEmail = "ameliah@mail.com",
                OwnerAddress = "159 Palm St",
                OwnerCity = "Federal Way",
                OwnerState = "WA",
                OwnerZipCode = "98003",
                Pets = []
            },
            new()
            {
                OwnerId = "17",
                OwnerName = "Ethan Young",
                OwnerPhone = "555-777-8888",
                OwnerEmail = "ethany@mail.com",
                OwnerAddress = "951 Hickory Rd",
                OwnerCity = "Edmonds",
                OwnerState = "WA",
                OwnerZipCode = "98020",
                Pets = []
            }
        };
    }
}
