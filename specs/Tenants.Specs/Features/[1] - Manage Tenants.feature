Feature: Feature - [1] - Manage Tenants
    As a service manager
    I need to manage tenants
    To keep control of the service

Scenario: Scenario - 1.1 - Add tenants

    Given these tenants don't exist:
        | Email               |
        | tenant-a@server.com |
        | tenant-b@server.com |

    When I add tenants:
        | Email               | Name         |
        | tenant-a@server.com | Add Tenant A |
        | tenant-b@server.com | Add Tenant B |

    Then when querying for "Add" tenants I get these:
        | Email               | Name         |
        | tenant-a@server.com | Add Tenant A |
        | tenant-b@server.com | Add Tenant B |

Scenario Outline: Scenario - 1.2 - Avoid duplicate tenant name

    Given tenant "<Email>" does not exist
    When I add tenant "<Email>", "<Name>"
    Then I can't add another tenant "<Email>", "<Name>"

    Examples: 
        | Email               | Name            |
        | tenant-c@server.com | Unique Tenant C |
        | tenant-d@server.com | Unique Tenant D |

Scenario: Scenario - 1.3 - Modify tenants

    Given these tenants don't exist:
        | Email                        |
        | tenant-e@server.com          |
        | tenant-f@server.com          |
        | tenant-e-modified@server.com |
        | tenant-f-modified@server.com |


    And I add tenants:
        | Email               | Name            |
        | tenant-e@server.com | Insert Tenant E |
        | tenant-f@server.com | Insert Tenant F |

    When I modify the tenants like so:
        | FindEmail           | Email                        | Name              |
        | tenant-e@server.com | tenant-e-modified@server.com | Modified Tenant E |
        | tenant-f@server.com | tenant-f-modified@server.com | Modified Tenant F |

    Then when querying for "Modified" tenants I get these:
        | Email                        | Name              |
        | tenant-e-modified@server.com | Modified Tenant E |
        | tenant-f-modified@server.com | Modified Tenant F |


Scenario: Scenario - 1.4 - Avoid duplicate email when modifying tenant

    Given these tenants don't exist:
        | Email               |
        | tenant-g@server.com |
        | tenant-h@server.com |


    And I add tenants:
        | Email               | Name            |
        | tenant-g@server.com | Insert Tenant G |
        | tenant-h@server.com | Insert Tenant H |

    Then I get error "DuplicateByEmailError" when trying to modify tenant's email from "tenant-g@server.com" to "tenant-h@server.com":


Scenario: Scenario - 1.5 - Remove tenant

    Given these tenants don't exist:
        | Email               |
        | tenant-i@server.com |
        | tenant-j@server.com |


    And I add tenants:
        | Email               | Name             |
        | tenant-i@server.com | Removed Tenant I |
        | tenant-j@server.com | Removed Tenant J |

    When I remove these tenants:
        | FindEmail           |
        | tenant-j@server.com |

    Then when querying for "Removed" tenants I get these:
        | Email               | Name             |
        | tenant-i@server.com | Removed Tenant I |
